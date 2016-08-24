var LineChart = function(div, chartname, dataobjectarray, customoptions){
	/*
		dataobjectarray example

		[{
			label: "data1",
			datax: [0,1,2,3],
			datay: [10,2,6,3]
		},{
			label: "data2",
			datax: [0,1,2,3],
			datay: [10,2,6,3]
		}]
		

	 */
	this.dataobjectarray = dataobjectarray;
	this.chart = $(div);

	var tempData = [];

	$.each(dataobjectarray, function(key, val){
		var tempDataObject = val;
		var dataxlength = tempDataObject.datax.length;
		var dataylength = tempDataObject.datay.length;
		var newData = [];
		for(var x = 0; x < dataxlength; x++){
			var tempx = tempDataObject.datax[x];
			var tempy = 0;
			if(x < dataylength){
				tempy = tempDataObject.datay[x];
			}
			newData.push({x : tempx, y : tempy});
		}
		tempData.push({
			 label: tempDataObject.label,
			 backgroundColor: randomRGBAColor(10,255, 0.3),
			 borderColor: randomRGBAColor(10,255, 0.3),
			 pointBorderColor: randomRGBAColor(10,200, 0.3),
			 pointHoverBackgroundColor: randomRGBAColor(20,255, 0.1),
	         pointHoverBorderColor: randomRGBAColor(30,240, 0.1),
			 data: newData
		});
	})


	this.data = { datasets: tempData };

	this.options = {
		animation : false, 
		maintainAspectRatio: false,
	    scales: {
	        xAxes: [{
	            type: 'linear',
	            position: 'bottom',
	            ticks: {
	                stepSize: 1
	            }
	        }],
	        yAxes: [{
	            type: 'linear',
	            position: 'left',
	        }]
	    }
	}

	if(customoptions !== undefined){
		this.options = customoptions;
	}

	this.myLineChart = new Chart(this.chart, {
	    type: 'line',
	    data: this.data,
	    options: this.options
	});

	this.getChart = function(){
		return this.myLineChart;
	}

	this.updateChart = function(label, xIn, yIn){
		var index = 0;
		$.each(this.myLineChart.data.datasets, function(key, value){
			if(value.label == label){
				return;
			}
			index++;
		});

		this.myLineChart.data.datasets[index].data.push({x: xIn, y: yIn});
		this.myLineChart.update();
	}

	this.resetChart = function(dataobjectarray2, setScaleX, setScaleY){
		this.myLineChart.destroy();
		this.data = {};
		var tempData = [];
		if(dataobjectarray2 !== undefined){
			$.each(dataobjectarray2, function(key, val){
				var tempDataObject = val;
				var dataxlength = tempDataObject.datax.length;
				var dataylength = tempDataObject.datay.length;
				var newData = [];
				for(var x = 0; x < dataxlength; x++){
					var tempx = tempDataObject.datax[x];
					var tempy = 0;
					if(x < dataylength){
						tempy = tempDataObject.datay[x];
					}
					newData.push({x : tempx, y : tempy});
				}
				tempData.push({
					 label: tempDataObject.label,
					 backgroundColor: randomRGBAColor(0,11, 0.1),
					 borderColor: randomRGBAColor(10,255, 1),
					 pointBorderColor: randomRGBAColor(10,200, 1),
					 pointHoverBackgroundColor: randomRGBAColor(0,11, 0.1),
		             pointHoverBorderColor: randomRGBAColor(30,240, 0.1),
					 data: newData
				});
			})
		}
		

		this.data = { datasets: tempData };
		this.myLineChart = new Chart(this.chart, {
		    type: 'line',
		    data: this.data,
		    options: this.options
		});
	}
}

function randomRGBAColor(min, max, opacity){
	var r = Math.floor(Math.random() * (max - min + 1)) + min; 
	var g = Math.floor(Math.random() * (max - min + 1)) + min;
	var b = Math.floor(Math.random() * (max - min + 1)) + min;

	return "rgba(" + r + "," + g + "," + b + "," + opacity + ")"; 
}