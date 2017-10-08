angular.module('TodoTrackerApp', [])
    .controller('TodoController', function ($scope, $http, $location, $window) {
        
    $scope.todoModel = {};
    $scope.message = '';
    $scope.result = "color-default";
    $scope.isViewLoading = false;
    $scope.ListTodo = null;
    getallData();
    //******=========Get All Todo=========******  
    function getallData() {
        //debugger;  
        $http.get('/Todo/GetAllActive')
              .then(function (response) {
                  $scope.ListTodo = response.data;

              })
          .catch(function (response) {
                 console.log(response);
                $scope.errors = [];
                $scope.message = 'Unexpected Error while loading data!!';
                $scope.result = "color-red";
                console.log($scope.message);
            });
    };
    //******=========Get Single Todo=========******  
    $scope.getTodo = function (todoModel) {
   
        $http.get('/Todo/GetActiveTodo/' + todoModel.TodoID)
            .then(function (response) {
                $scope.todoModel = response.data;
                getallData();
                console.log(response.data);
                $scope.message = 'Todo ' +todoModel.TodoID + ' Retreived';
            })
            .catch(function (response) {
                console.log(response.error);
                $scope.errors = [];
                $scope.message = 'Unexpected Error while loading a todo!!';
                $scope.result = "color-red";
                console.log($scope.message);
        });
    };
    //******=========Save Todo=========******  
    $scope.saveTodo = function () {
        $scope.isViewLoading = true;
        $http(
        {
            method: 'POST',
            url: '/Home/Insert',
            data: $scope.todoModel
        }).success(function (data, status, headers, config) {
            $scope.errors = [];
            if (data.success === true) {
                $scope.message = 'Form data Saved!';
                $scope.result = "color-green";
                getallData();
                $scope.todoModel = {};
                console.log(data);
            }
            else {
                $scope.errors = data.errors;
            }
        }).error(function (data, status, headers, config) {
            $scope.errors = [];
            $scope.message = 'Unexpected Error while saving data!!';
            console.log($scope.message);
        });
        getallData();
        $scope.isViewLoading = false;
    };
    //******=========Edit Todo=========******  
    $scope.updateTodo = function () {
        //debugger;  
        $scope.isViewLoading = true;
        $http(
        {
            method: 'POST',
            url: '/Home/Update',
            data: $scope.todoModel
        }).success(function (data, status, headers, config) {
            $scope.errors = [];
            if (data.success === true) {
                $scope.todoModel = null;
                $scope.message = 'Form data Updated!';
                $scope.result = "color-green";
                getallData();
                console.log(data);
            }
            else {
                $scope.errors = data.errors;
            }
        }).error(function (data, status, headers, config) {
            $scope.errors = [];
            $scope.message = 'Unexpected Error while saving data!!';
            console.log($scope.message);
        });
        $scope.isViewLoading = false;
    };
    //******=========Delete Todo=========******  
    $scope.deleteTodo = function (todoModel) {
        //debugger;  
        var IsConf = confirm('You are about to delete TODO Number # ' + todoModel.TodoID + '. Are you sure?');
        if (IsConf) {
            $http.delete('/Todo/DeleteTodo/' + todoModel.TodoID)
                .then(function (response)
                {
                $scope.errors = [];
                if (response.data.success === true) {
                    $scope.message = todoModel.TodoID + ' deleted from record!!';
                    $scope.result = "color-red";
                    getallData();
                    console.log(response.data);
                    $window.location.reload();
                }
                else {
                    $scope.errors = response.data.errors;
                }
            }).catch(function (response) {
                $scope.errors = [];
                $scope.message = 'Unexpected Error while deleting a todo!!';
                console.log(response.error);
            });
        }
    };

        //******=========Complete Todo=========******  
    $scope.completeTodo = function (todoModel) {
        //debugger;  
        var IsConf = confirm('You are about to make complete TODO Number # ' + todoModel.TodoID + '. Are you sure?');
        if (IsConf) {

            $http(
            {
                method: 'POST',
                url: '/Todo/CompleteTodo',
                data: $scope.todoModel.TodoID
            })
                .then(function (response) {
                    $scope.errors = [];
                    if (response.data.success === true) {
                        $scope.message = todoModel.TodoID + ' completed !!';
                        $scope.result = "color-red";
                        getallData();
                        console.log(response.data);
                        $window.location.reload();
                    }
                    else {
                        $scope.errors = response.data.errors;
                    }
                }).catch(function (response) {
                    $scope.errors = [];
                    $scope.message = 'Unexpected Error while completing a todo!!';
                    console.log(response.error);
                });
        }
    };

}).config(function ($locationProvider) {
    $locationProvider.html5Mode(false);

});