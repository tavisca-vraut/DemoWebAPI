pipeline {
    agent any
    environment {
        REPO_PATH = 'https://github.com/tavisca-vraut/DemoWebAPI.git'
    }
    parameters {
        string(name: 'REPO_PATH', defaultValue: 'https://github.com/tavisca-vraut/DemoWebAPI.git')
        string(name: 'SOLUTION_PATH', defaultValue: 'DemoWebApp.sln')
        string(name: 'TEST_PATH', defaultValue: 'DemoTest/DemoTest.csproj')
        choice(name: 'JOB', choices:  ['Build' , 'Test'])
        string(name: 'PROJECT', defaultValue: 'DemoWebApp')
    }
    stages {
        stage ('Testing if the powershell commands work')
        {
            steps
            {
                powershell(script: 'echo "Hello1, $REPO_PATH"')
                powershell(script: 'echo "Hello2, {$params.REPO_PATH}"')
            }
        }
        // stage('Echo current directory')
        // {
        //     steps
        //     {
        //         powershell 'pwd'
        //     }
        // }
        // stage('Build') {
        //     steps {
        //         powershell 'echo "*********Starting Restore and Build***************'
        //         powershell 'dotnet restore ${SOLUTION_PATH} --source https://api.nuget.org/v3/index.json'
        //         powershell 'dotnet build ${SOLUTION_PATH} -p:Configuration=release -v:n'
        //         powershell 'echo "***************Recovery Finish********************"'
        //     }
        // }
        // stage('Test') {
        //     when
        //     {
        //         expression { params.JOB == 'Test' }
        //     }
            
        //     steps {
        //         powershell 'dotnet test ${TEST_PATH}'
        //     }
        // }
        // stage('Publish') {
        //     steps {
        //         powershell 'dotnet publish -o ./Published'
        //     }
        // }
    }
}
