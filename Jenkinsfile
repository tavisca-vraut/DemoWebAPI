pipeline {
    agent any
    parameters {
        string(name: 'REPO_PATH', defaultValue: 'https://github.com/tavisca-vraut/DemoWebAPI.git')
        string(name: 'SOLUTION_PATH', defaultValue: 'DemoWebApp.sln')
        string(name: 'TEST_PATH', defaultValue: 'DemoTest/DemoTest.csproj')
        choice(name: 'JOB', choices:  ['Build' , 'Test'])
        string(name: 'NUGET_REPO', defaultValue: 'https://api.nuget.org/v3/index.json')
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
                def restoreCommand = 'dotnet restore $env:SOLUTION_PATH --source $env:NUGET_REPO'
                def buildCommand = 'dotnet build $env:SOLUTION_PATH -p:Configuration=release -v:n'
                    
                powershell(script: 'echo "Hello1, $restoreCommand"')
                powershell(script: 'echo "Hello2, $helloCommand"')
                powershell(script: 'echo "Hello3, $env:REPO_PATH"')
                powershell(script: 'echo "Hello4, $env:SOLUTION_PATH"')

                powershell(script: 'echo "*********Starting Restore and Build***************')
                powershell(script: restoreCommand)
                powershell(script: buildCommand)
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
                powershell(script: 'dotnet publish -o ./Published')
            }
        }
    }
}
