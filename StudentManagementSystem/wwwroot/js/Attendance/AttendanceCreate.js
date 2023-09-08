angular.module('AttendanceModule', [])
    .controller('AttendancesController', function ($scope, $http) {
        $scope.courselist = [];
        $scope.levellist = [];
        $scope.grouplist = [];
        $scope.studentlist = [];
        $scope.statuslist = [];
        $scope.init = function (AttendanceDataList) {
            debugger;
            $scope.Date = new Date();
            $scope.courselist = AttendanceDataList.CourseData;
            $scope.levellist = AttendanceDataList.LevelData;
            $scope.grouplist = AttendanceDataList.GroupData;

            var data = [
                { Id: 1, Name: 'Absent' },
                { Id: 2, Name: 'Present' }
            ];
            $scope.statuslist = data;
        }
        $scope.onClickSearchBtn = function () {
            var data = {
                Date: $scope.Date,
                LevelId: $scope.levelId,
                CourseId: $scope.courseId,
                GroupId: $scope.groupId
            }

            $http.post('/attendance/getStudentData', data)
                .then(function (response) {
                    debugger;
                    var data = response.data;
                    $scope.studentlist = [];
                    angular.forEach(data, function (value, key) {
                        var dataObt = {
                            Id: value.id,
                            Name: value.name,
                            CourseId: value.courseId,
                            GroupId: value.groupId,
                            LevelId: value.levelId,
                            Status: $scope.statuslist.filter(x => x.Id == 1)[0],
                            Sn: ++key
                        };
                        $scope.studentlist.push(dataObt);
                    });
                    
                })
                .catch(function (error) {
                    // Handle any errors
                });
        }
    });