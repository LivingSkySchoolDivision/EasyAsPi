pipeline {
    agent any
    environment {
        REPO_API = "easyaspi/api"
        PRIVATE_REPO_API = "${PRIVATE_DOCKER_REGISTRY}/${REPO_API}"
        TAG = "${BUILD_TIMESTAMP}"
    }
    stages {
        stage('Git clone') {
            steps {
                git branch: 'main',
                    url: 'https://github.com/LivingSkySchoolDivision/EasyAsPi'
            }
        }
        stage('Docker build - API') {
            steps {
                dir("src/Backend") {
                    sh "docker build -f Dockerfile-API -t ${PRIVATE_REPO_API}:latest -t ${PRIVATE_REPO_API}:${TAG} ."
                }
            }
        }
        stage('Docker push') {
            steps {
                sh "docker push ${PRIVATE_REPO_API}:${TAG}"
                sh "docker push ${PRIVATE_REPO_API}:latest"
            }
        }
    }
    post {
        always {
            deleteDir()
        }
    }
}