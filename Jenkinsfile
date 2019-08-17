// Globals
def image

pipeline 
{
    agent any
    parameters 
    {
        string(name: 'SOLUTION_PATH', defaultValue: 'DemoWebApp.sln')
        string(name: 'TEST_PATH', defaultValue: 'DemoTest/DemoTest.csproj', description: 'Relative Path of the .csproj file of test project')
        string(name: 'APPLICATION_NAME', defaultValue: 'DemoWebApp', description: 'Name of the project that you want to test/deploy/etc.')
        string(name: 'JOB_NAME', defaultValue: 'Demo-WebApi-Test', description: 'Name of the current job that is going to run the pipeline.')
        string(name: 'DOCKER_HUB_USERNAME')
        string(name: 'DOCKER_HUB_PASSWORD')
        string(name: 'DOCKER_IMAGE_NAME', defaultValue: 'demo-web-app-test')
        string(name: 'DOCKER_IMAGE_TAG', defaultValue: 'latest')
        choice(name: 'JOB', choices:  ['Test' , 'Build', 'Create Image'])
    }
    environment
    {
        // DON'T EDIT UNLESS YOU KNOW WHAT YOU ARE DOING
        nugetRepository = 'https://api.nuget.org/v3/index.json'

        restoreCommand = "dotnet restore ${env.SOLUTION_PATH} --source ${env.nugetRepository}"
        buildCommand = "dotnet build ${env.SOLUTION_PATH} -p:Configuration=release -v:n"

        artifactsDirectory = "MyArtifacts"
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
                powershell(script: "dotnet publish ${env.APPLICATION_NAME} -c Release -o ${env.artifactsDirectory} --no-restore")
            }
        }
        stage('Archive')
        {
            steps
            {
                // powershell "echo 'compress-archive ${env.APPLICATION_NAME}/artifacts publish.zip -Update'"
                powershell "compress-archive ${env.APPLICATION_NAME}/${env.artifactsDirectory}/*.* publish.zip -Update"
                archiveArtifacts artifacts: 'publish.zip'    
            }
        }
        stage('Retrieve artifact')
        {
            steps
            {
                copyArtifacts filter: 'publish.zip', projectName: 'Demo-WebApi-Test'
                powershell(script: "expand-archive publish.zip ./${env.artifactsDirectory} -Force")
            }
        }
        stage('Set-up for docker image creation')
        {
            steps
            {
                powershell "mv Dockerfile ${env.artifactsDirectory}"
            }
        }
        stage('Build docker image')
        {
            // steps
            // {
            //     powershell "docker build -t ${env.DOCKER_HUB_USERNAME}/${env.DOCKER_IMAGE_NAME}:${env.DOCKER_IMAGE_TAG} ${env.artifactsDirectory}/"
            // }
            dir("${env.artifactsDirectory}")
            {
                docker.withRegistry('docker.io', 'docker-hub-credentials') 
                {
                    image = docker.build("${env.DOCKER_HUB_USERNAME}/${env.DOCKER_IMAGE_NAME}:${env.DOCKER_IMAGE_TAG}")
                }
            }
        }
        stage('Push Docker image to DockerIO registry')
        {
            steps
            {
                // powershell "echo 'docker login -u ${env.DOCKER_HUB_USERNAME} -p ${env.DOCKER_HUB_PASSWORD} docker.io'"
                // powershell "docker login -u ${env.DOCKER_HUB_USERNAME} -p ${env.DOCKER_HUB_PASSWORD} docker.io"
                // powershell "docker push ${env.DOCKER_HUB_USERNAME}/${env.DOCKER_IMAGE_NAME}:${env.DOCKER_IMAGE_TAG}"
                image.push()
            }
        }
    }
    post
    {
        always
        {
            deleteDir()
        }
    }
}
