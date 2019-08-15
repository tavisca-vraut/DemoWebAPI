pipeline {
    agent any
    parameters {
        string(name: 'REPO_PATH', defaultValue: 'https://github.com/tavisca-vraut/DemoWebAPI.git')
        string(name: 'SOLUTION_PATH', defaultValue: 'DemoWebApp.sln')
        string(name: 'TEST_PATH', defaultValue: 'DemoTest/DemoTest.csproj')
        choice(name: 'JOB', choices:  ['Build' , 'Test'])
        string(name: 'PROJECT_TO_BE_PUBLISHED', defaultValue: 'DemoWebApp')
        string(name: 'NUGET_REPO', defaultValue: 'https://api.nuget.org/v3/index.json')
    }
    environment
    {
        restoreCommand = 'dotnet restore $env:SOLUTION_PATH --source $env:NUGET_REPO'
        buildCommand = 'dotnet build $env:SOLUTION_PATH -p:Configuration=release -v:n'
    }
    stages {
        stage('list current directory')
        {
            steps
            {
                powershell(script: 'ls')
            }
        }
        stage('Build') {
            steps {    
                powershell(script: 'echo "-----------Commands to be executed-------------"')
                powershell(script: 'echo "Hello1, $env:restoreCommand"')
                powershell(script: 'echo "Hello2, $env:buildCommand"')
                powershell(script: 'echo "-----------End of List-------------"')

                powershell(script: 'echo "*********Starting Restore and Build***************')
                powershell(script: '$env:restoreCommand')
                powershell(script: '$env:buildCommand')
                powershell(script: 'echo "***************Recovery Finish********************"')
            }
        }
        stage('Test') {
            when
            {
                expression { params.JOB == 'Test' }
            }
            
            steps {
                powershell(script: 'dotnet test $env:TEST_PATH')
            }
        }
        stage('Publish') {
            steps {
                powershell(script: 'dotnet publish $env:PROJECT_TO_BE_PUBLISHED -c Release')
            }
        }
    }
}
