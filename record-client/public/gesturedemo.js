var accelerometer = null;

var events = [];

var classes = ['_flick', '_shield', '_throw'];
var class_indices = {
	_flick: 0,
	_shield: 1,
	_throw: 2
}

var sounds = {
	_flick: new Audio('flick.mp3'),
	_shield: new Audio('shield.mp3'),
	_throw: new Audio('throw.mp3')
}

var COOLDOWN = 15;
var THRESHOLD = 0.4;

var cooldown = {};
var activation = {};
for (var i = 0; i < classes.length; i++) {
	cooldown[classes[i]] = 0;
	activation[classes[i]] = 0;
}

var model;

RUN_ONE_IN_N = 1
run_index = 0

var TIMESTEPS = 20;

tf.loadLayersModel('/model_js/model.json').then(function(m) {
	model = m;
	$('#model_status').text('Model ready!');
	$('#start').show();

	$('#start').click(function() {
		$('#start').hide();
		$('#instructions').show();
	});

	accelerometer = new Accelerometer({frequency: 10});
	accelerometer.addEventListener('reading', e => {
	  events.push([accelerometer.x, accelerometer.y, accelerometer.z]);
	  while (events.length > TIMESTEPS) {
	  	events.splice(0, 1);
	  }

	  if (run_index == 0) {
	  	model.predict(tf.tensor3d([events])).data().then(data => {
	  	for (var i = 0; i < classes.length; i++) {
	  		curr_class = classes[i];
	  		if (cooldown[curr_class] > 0) {
	  			cooldown[curr_class] -= 1;
	  		} else if (data[class_indices[curr_class]] >= THRESHOLD) {
	  			if (activation[curr_class] == 0) {
	  				activation[curr_class] = 1;
	  			} else {
	  				activation[curr_class] = 0;
	  				sounds[curr_class].play();
	  				cooldown[curr_class] = COOLDOWN;
	  			}
	  		} else {
	  			activation[curr_class] = 0;
	  		}
	   	  }
	    });
	  }

	  run_index = (run_index + 1) % RUN_ONE_IN_N
	  
	});

	accelerometer.start();
});