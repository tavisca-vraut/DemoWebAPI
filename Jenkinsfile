pipeline 
{
    agent any
    parameters 
    {
        string(name: 'REPO_PATH', defaultValue: 'https://github.com/tavisca-vraut/DemoWebAPI.git')
        string(name: 'SOLUTION_PATH', defaultValue: 'DemoWebApp.sln')
        string(name: 'TEST_PATH', defaultValue: 'DemoTest/DemoTest.csproj')
        string(name: 'PROJECT_NAME', defaultValue: 'DemoWebApp', description: 'Name of the project that you want to test/deploy/etc.')
        choice(name: 'JOB', choices:  ['Test' , 'Build'])
    }
    environment
    {
        nugetRepository = 'https://api.nuget.org/v3/index.json'
        restoreCommand = 'dotnet restore $env:SOLUTION_PATH --source $env:nugetRepository'
        buildCommand = 'dotnet build $env:SOLUTION_PATH -p:Configuration=release -v:n'
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
                powershell(script: 'dotnet test $env:TEST_PATH')
            }
        }
        stage('Publish') 
        {
            steps 
            {
                powershell(script: 'dotnet publish $env:PROJECT_NAME -c Release -o artifacts')
            }
        }
        stage('Archive')
        {
            steps
            {
                powershell(script: 'compress-archive $env:PROJECT_NAME/artifacts publish.zip -Update')
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
    }
    // post
    // {
        // always
        // {
        //     deleteDir()
        // }
    // }
}
