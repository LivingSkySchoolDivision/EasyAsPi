pipeline {
    agent any
    environment {
        REPO_API = "easyaspi/api"
        REPO_WEB = "easyaspi/web"
        PRIVATE_REPO_API = "${PRIVATE_DOCKER_REGISTRY}/${REPO_API}"
        PRIVATE_REPO_WEB = "${PRIVATE_DOCKER_REGISTRY}/${REPO_WEB}"
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
        stage('Docker build - Web') {
            steps {
                dir("src/Backend") {
                    sh "docker build -f Dockerfile-Web -t ${PRIVATE_REPO_WEB}:latest -t ${PRIVATE_REPO_WEB}:${TAG} ."
                }
            }
        }
        stage('Docker push') {
            steps {
                sh "docker push ${PRIVATE_REPO_API}:${TAG}"
                sh "docker push ${PRIVATE_REPO_API}:latest"
                sh "docker push ${PRIVATE_REPO_WEB}:${TAG}"
                sh "docker push ${PRIVATE_REPO_WEB}:latest"
            }
        }
    }
    post {
        always {
            deleteDir()
        }
    }
}