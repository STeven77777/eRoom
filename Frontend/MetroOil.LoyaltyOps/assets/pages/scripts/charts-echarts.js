jQuery(document).ready(function() {
    // ECHARTS

    debugger
    require.config({
        paths: {
            echarts: '/assets/global/plugins/echarts/'
        }
    });

    // DEMOS
    require(
        [
            'echarts',
            'echarts/chart/bar'
        ],
        function(ec) {debugger
            //--- BAR ---
            var myChart = ec.init(document.getElementById('echarts_bar'));
            myChart.setOption({
                tooltip: {
                    trigger: 'axis'
                },
                legend: {
                    data: ['Cost', 'Expenses']
                },
                toolbox: {
                    show: true,
                    feature: {
                        mark: {
                            show: true
                        },
                        dataView: {
                            show: true,
                            readOnly: false
                        },
                        magicType: {
                            show: true,
                            type: ['line', 'bar']
                        },
                        restore: {
                            show: true
                        },
                        saveAsImage: {
                            show: true
                        }
                    }
                },
                calculable: true,
                xAxis: [{
                    type: 'category',
                    data: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
                }],
                yAxis: [{
                    type: 'value',
                    splitArea: {
                        show: true
                    }
                }],
                series: [{
                    name: 'Cost',
                    type: 'bar',
                    data: [2.0, 4.9, 7.0, 23.2, 25.6, 76.7, 135.6, 162.2, 32.6, 20.0, 6.4, 3.3]
                }, {
                    name: 'Expenses',
                    type: 'bar',
                    data: [2.6, 5.9, 9.0, 26.4, 28.7, 70.7, 175.6, 182.2, 48.7, 18.8, 6.0, 2.3]
                }]
            });
        }
    );
});