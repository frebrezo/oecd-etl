package org.wonkim.oecdetl.batch;

import org.springframework.batch.core.Job;
import org.springframework.batch.core.Step;
import org.springframework.batch.core.job.builder.JobBuilder;
import org.springframework.batch.core.repository.JobRepository;
import org.springframework.batch.core.step.builder.StepBuilder;
import org.springframework.batch.core.step.tasklet.Tasklet;
import org.springframework.batch.item.ItemProcessor;
import org.springframework.batch.item.ItemReader;
import org.springframework.batch.item.ItemWriter;
import org.springframework.batch.support.transaction.ResourcelessTransactionManager;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.boot.context.properties.ConfigurationProperties;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.annotation.Primary;
import org.springframework.jdbc.core.namedparam.NamedParameterJdbcTemplate;
import org.springframework.jdbc.datasource.DriverManagerDataSource;
import org.springframework.transaction.PlatformTransactionManager;
import org.wonkim.oecdetl.external.OecdDataResponseTransformer;
import org.wonkim.oecdetl.external.OecdDataServiceAgent;
import org.wonkim.oecdetl.external.dto.OecdDataResponse;
import org.wonkim.oecdetl.model.GenderEnt1;

import javax.sql.DataSource;
import java.util.List;
import java.util.UUID;

// https://www.baeldung.com/introduction-to-spring-batch
// https://www.baeldung.com/spring-boot-spring-batch
// https://stackoverflow.com/questions/64871126/spring-batch-datasource-for-batch-and-datasource-for-step
@Configuration
public class BatchConfig {
    @Value("GENDER_ENT1")
    private String dataSetId;

    @Primary
    @Bean(value = "dataSource")
    @ConfigurationProperties(prefix = "spring.datasource")
    public DataSource getDataSource() {
        DriverManagerDataSource dataSource = new DriverManagerDataSource();
        return dataSource;
    }

    @Bean
    public Tasklet truncateGenderEnt1(NamedParameterJdbcTemplate jdbcTemplate) {
        return new TruncateGenderEnt1Tasklet(jdbcTemplate);
    }

    @Bean
    public ItemReader<OecdDataResponse> oecdDataResponseItemReader() {
        OecdDataServiceAgent serviceAgent = new OecdDataServiceAgent(dataSetId);
        return new OecdDataResponseItemReader(serviceAgent);
    }

    @Bean
    public ItemProcessor<OecdDataResponse, List<GenderEnt1>> oecdDataResponseItemProcessor() {
        OecdDataResponseTransformer transformer = new OecdDataResponseTransformer();
        return new GenderEnt1ItemProcessor(transformer);
    }

    // https://www.petrikainulainen.net/programming/spring-framework/spring-batch-tutorial-writing-information-to-a-database-with-jdbc/
    @Bean
    public ItemWriter<List<GenderEnt1>> genderEnt1ItemWriter(NamedParameterJdbcTemplate jdbcTemplate) {
        return new GenderEnt1ItemWriter(jdbcTemplate);
    }

    @Bean
    protected Step step1(JobRepository jobRepository,
                         PlatformTransactionManager transactionManager,
                         Tasklet truncateGenderEnt1) {
        return new StepBuilder("step1", jobRepository)
                .tasklet(truncateGenderEnt1, transactionManager)
                .build();
    }

    @Bean
    protected Step step2(JobRepository jobRepository,
                         PlatformTransactionManager transactionManager,
                         ItemReader<OecdDataResponse> oecdDataResponseItemReader,
                         ItemProcessor<OecdDataResponse, List<GenderEnt1>> oecdDataResponseItemProcessor,
                         ItemWriter<List<GenderEnt1>> genderEnt1ItemWriter) {
        return new StepBuilder("step2", jobRepository)
                .<OecdDataResponse, List<GenderEnt1>> chunk(1, transactionManager)
                .reader(oecdDataResponseItemReader)
                .processor(oecdDataResponseItemProcessor)
                .writer(genderEnt1ItemWriter)
                .build();
    }

    @Bean(name = "firstBatchJob")
    public Job job(JobRepository jobRepository,
                   @Qualifier("step1") Step step1,
                   @Qualifier("step2") Step step2) {
        return new JobBuilder(UUID.randomUUID().toString(), jobRepository)
                .preventRestart()
                .start(step1)
                .next(step2)
                .build();
    }

    @Bean(name = "transactionManager")
    public PlatformTransactionManager getTransactionManager() {
        return new ResourcelessTransactionManager();
    }
}
