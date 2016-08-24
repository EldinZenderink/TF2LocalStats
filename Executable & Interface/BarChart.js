var BarChart = function(div, chartname, dataobjectarray, customoptions){
	/*
		dataobjectarray example

		[{
			label: "data1",
			data: 20
		},{
			label: "data2",
			data: 10
		}]
		

	 */
	
	this.dataobjectarray = dataobjectarray;
	this.chart = $(div);


	var tempData = [];
	var tempLabels = [];

	var backgroundColorArray = [];
	var borderColorArray = [];
	var labelBackgroundsArray = [];
	var tempLengthOfLabels = tempLabels.length;
	$.each(this.dataobjectarray, function(index, val){
		tempData.push(val.data);
		tempLabels.push(val.label);

		backgroundColorArray.push(randomRGBAColor(50, 100, 0.5));
		borderColorArray.push(randomRGBAColor(50, 200, 1));
		labelBackgroundsArray.push('rgba(255,255,255,0.1)');
	})


	this.data = { 
		labelBackgrounds: labelBackgroundsArray,
		labels: tempLabels,
		datasets: [{
			label: chartname,
            backgroundColor: backgroundColorArray,
            borderColor: borderColorArray,
            borderWidth: 1,
			data: tempData
		}]
	};


	this.options = {
		showYLabels: 4,
		maintainAspectRatio: false,
		animation : false, 
	    scales: {
	        xAxes: [{
	            ticks: {
                    min: 0,
                    stepSize: 1
                }
	        }],
	        yAxes: [{
	            ticks: {
                    min: 0
                }
	        }]
	    }
	}

	if(customoptions !== undefined){
		this.options = customoptions;
	}

	this.myBarChart = new Chart(this.chart, {
	    type: 'bar',
	    data: this.data,
	    options: this.options
	});

	this.chart.css("background-color", "rgba(255,255,255,1)");

	this.getChart = function(){
		return this.myBarChart;
	}

	this.updateChart = function(newdataobject){
		this.chart.css("background-color", "rgba(255,255,255,1)");
		this.myBarChart.data.datasets[0].data = newdataobject;
		this.myBarChart.update();
	}

	this.updateOptions = function(option, value){
		this.options[option] = value;
		this.resetChart(this.dataobjectarray);
	}

	this.setYScale = function(yScale){
		this.options.showYLabels = yScale;
		this.resetChart(this.dataobjectarray);
	}		

	this.resetChart = function(dataobjectarray2){
		this.myBarChart.destroy();
		this.data = {};
		
		var tempData = [];
		var tempLabels = [];

		var backgroundColorArray = [];
		var borderColorArray = [];
		var labelBackgroundsArray = [];
		if(dataobjectarray2 !== undefined){
			$.each(dataobjectarray2, function(index, val){
				tempData.push(val.data);
				tempLabels.push(val.label);
				backgroundColorArray.push(randomRGBAColor(50, 100, 0.5));
				borderColorArray.push(randomRGBAColor(50, 200, 1));
				labelBackgroundsArray.push('rgba(255,255,255,0)');

			})
		}
		

		var tempLengthOfLabels = tempLabels.length;

		this.data = { 
			labelBackgrounds: labelBackgroundsArray,
			labels: tempLabels,
			datasets: [{
				labelBackgrounds: labelBackgroundsArray,
				label: chartname,
	            backgroundColor: backgroundColorArray,
	            borderColor: borderColorArray,
	            borderWidth: 1,
				data: tempData
			}]
		};

		this.myBarChart = new Chart(this.chart, {
		    type: 'bar',
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