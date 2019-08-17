pipeline 
{
    agent any
    parameters 
    {
        string(name: 'SOLUTION_PATH', defaultValue: 'DemoWebApp.sln')
        string(name: 'TEST_PATH', defaultValue: 'DemoTest/DemoTest.csproj', description: 'Relative Path of the .csproj file of test project')
        string(name: 'PROJECT_NAME', defaultValue: 'DemoWebApp', description: 'Name of the project that you want to test/deploy/etc.')
        string(name: 'JOB_NAME', defaultValue: 'Demo-WebApi-Test', description: 'Name of the current job that is going to run the pipeline.')
        string(name: 'DOCKER_HUB_USERNAME')
        string(name: 'DOCKER_HUB_PASSWORD')
        string(name: 'DOCKER_IMAGE_NAME', defaultValue: 'demo-web-app-test')
        string(name: 'DOCKER_IMAGE_TAG', defaultValue: 'latest')
        choice(name: 'JOB', choices:  ['Test' , 'Build', 'Create Image'])
    }
    environment
    {
        nugetRepository = 'https://api.nuget.org/v3/index.json'
        restoreCommand = "dotnet restore ${env.SOLUTION_PATH} --source ${env.nugetRepository}"
        buildCommand = "dotnet build ${env.SOLUTION_PATH} -p:Configuration=release -v:n"
    }
    stages 
    {
        stage('Build') 
        {
            steps
            {    
                powershell(script: "echo '*********Starting Restore and Build***************'")
                powershell(script: '$env:restoreCommand')
                powershell(script: '$env:buildCommand')
                powershell(script: "echo '***************Recovery Finish********************'")
            }
        }
        stage('Test') 
        {
            when
            {
                expression { params.JOB == 'Test'}
            }
            
            steps 
            {
                powershell(script: "dotnet test ${env.TEST_PATH}")
            }
        }
        stage('Publish') 
        {
            steps 
            {
                powershell(script: "dotnet publish ${env.PROJECT_NAME} -c Release -o artifacts --no-restore")
            }
        }
        stage('Archive')
        {
            steps
            {
                // powershell "echo 'compress-archive ${env.PROJECT_NAME}/artifacts publish.zip -Update'"
                powershell "compress-archive ${env.PROJECT_NAME}/artifacts publish.zip -Update"
                archiveArtifacts artifacts: 'publish.zip'    
            }
        }
        stage('Retrieve artifact')
        {
            steps
            {
                copyArtifacts filter: 'publish.zip', projectName: 'Demo-WebApi-Test'
                powershell(script: 'expand-archive publish.zip ./ -Force')
            }
        }
        stage('Set-up for docker image creation')
        {
            steps
            {
                powershell 'mv Dockerfile artifacts'
            }
        }
        stage('Build docker image')
        {
            steps
            {
                // powershell "echo 'docker build -t ${env.DOCKER_USERNAME}/${env.DOCKER_IMAGE_NAME}:${env.DOCKER_IMAGE_TAG} artifacts/'"
                powershell "docker build -t ${env.DOCKER_USERNAME}/${env.DOCKER_IMAGE_NAME}:${env.DOCKER_IMAGE_TAG} artifacts/"
            }
        }
        stage('Push Docker image to DockerIO registry')
        {
            steps
            {
                // powershell "echo 'docker login -u ${env.DOCKER_HUB_USERNAME} -p ${env.DOCKER_HUB_PASSWORD}'"
                powershell "docker login -u ${env.DOCKER_HUB_USERNAME} -p ${env.DOCKER_HUB_PASSWORD}"
                powershell "docker push ${env.DOCKER_USERNAME}/${env.DOCKER_IMAGE_NAME}:${env.DOCKER_IMAGE_TAG}"
            }
        }
        // stage('Build image') 
        // {
        //     app = docker.build("getintodevops/hellonode")
        // }

        // stage('Push image') 
        // {
        //     docker.withRegistry('https://registry.hub.docker.com', 'docker-hub-credentials') 
        //     {
        //         app.push("${env.BUILD_NUMBER}")
        //         app.push("latest")
        //     }
        // }
    }
    // post
    // {
        // always
        // {
        //     deleteDir()
        // }
    // }
}
