/*
 Add your docker credentials to jenkins credentials manager. Don't forget to give it an ID.
 When, prompted for ID in parameters, enter it.
*/


// Globals
def CustomImage

pipeline 
{
    agent any
    parameters 
    {
        string(name: 'JOB_NAME', defaultValue: 'Demo-WebApi-Test', description: 'Name of the current job that is going to run the pipeline.')

        string(name: 'APPLICATION_NAME', defaultValue: 'DemoWebApp', description: 'Name of the project that you want to test/deploy/etc.')
        string(name: 'SOLUTION_PATH', defaultValue: 'DemoWebApp.sln')
        string(name: 'TEST_PATH', defaultValue: 'DemoTest/DemoTest.csproj', description: 'Relative Path of the .csproj file of test project')
        
        string(name: 'DOCKER_HUB_USERNAME', defaultValue: 'vighnesh153')
        string(name: 'DOCKER_HUB_CREDENTIALS_ID', defaultValue: 'docker-hub-credentials')
        string(name: 'DOCKER_IMAGE_NAME', defaultValue: 'demo-web-app-test', description: 'Name of the image to be created')
        string(name: 'DOCKER_IMAGE_TAG', defaultValue: 'latest', description: 'Release information')
        choice(name: 'JOB', choices:  ['Test' , 'Build', 'Create Image'])
    }
    environment
    {
        // DON'T EDIT UNLESS YOU KNOW WHAT YOU ARE DOING
        nugetRepository = 'https://api.nuget.org/v3/index.json'

        restoreCommand = "dotnet restore ${env.SOLUTION_PATH} --source ${env.nugetRepository}"
        buildCommand = "dotnet build ${env.SOLUTION_PATH} -p:Configuration=release -v:n --no-restore"

        artifactsDirectory = "MyArtifacts"
    }
    // stages
    // {
    //     stage ('Test something')
    //     {
    //         steps
    //         {
    //             withCredentials([usernamePassword(credentialsId: "${env.DOCKER_HUB_CREDENTIALS_ID}", usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD')]) 
    //             {
    //                 powershell "echo 'Hello, ${USERNAME} . Please work'"
    //             }
    //         }
    //     }
    // }
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
        // stage('Archive')
        // {
        //     steps
        //     {
        //         // powershell "echo 'compress-archive ${env.APPLICATION_NAME}/artifacts publish.zip -Update'"
        //         powershell "compress-archive ${env.APPLICATION_NAME}/${env.artifactsDirectory}/*.* publish.zip -Update"
        //         archiveArtifacts artifacts: 'publish.zip'    
        //     }
        // }
        // stage('Retrieve artifact')
        // {
        //     steps
        //     {
        //         copyArtifacts filter: 'publish.zip', projectName: 'Demo-WebApi-Test'
        //         powershell(script: "expand-archive publish.zip ./${env.artifactsDirectory} -Force")
        //     }
        // }
        stage('Set-up for docker CustomImage creation')
        {
            steps
            {
                powershell "mv Dockerfile ${env.APPLICATION_NAME}/${env.artifactsDirectory}"
            }
        }
        stage('Build Custom Docker Image')
        {
            steps 
            {
                script 
                {
                    dir("${env.APPLICATION_NAME}/${env.artifactsDirectory}") 
                    {
                        docker.withRegistry('https://www.docker.io/', "${env.DOCKER_HUB_CREDENTIALS_ID}") 
                        {
                            powershell "echo '${env.DOCKER_HUB_USERNAME}/${env.DOCKER_IMAGE_NAME}:${env.DOCKER_IMAGE_TAG}'"
                            powershell "ls"
                            CustomImage = docker.build("${env.DOCKER_HUB_USERNAME}/${env.DOCKER_IMAGE_NAME}:${env.DOCKER_IMAGE_TAG}")
                        }
                    }
                }
            }
        }
        stage('Push Docker CustomImage to DockerIO registry') 
        {
            steps {
                script {
                    CustomImage.push()
                }
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
