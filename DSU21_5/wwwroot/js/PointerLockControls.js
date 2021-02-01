import {
	Euler,
	EventDispatcher,
	Vector3
} from '/js/three.module.js';

var PointerLockControls = function (camera, domElement) {

	if (domElement === undefined) {

		console.warn('THREE.PointerLockControls: The second parameter "domElement" is now mandatory.');
		domElement = document.body;

	}

	this.domElement = domElement;
	this.isLocked = false;

	// Set to constrain the pitch of the camera
	// Range is 0 to Math.PI radians
	this.minPolarAngle = 0; // radians
	this.maxPolarAngle = Math.PI; // radians

	//
	// internals
	//

	

	var scope = this;
	var changeEvent = { type: 'change' };
	var lockEvent = { type: 'lock' };
	var unlockEvent = { type: 'unlock' };
	var unlockForArtEvent = { type: 'unlockForArt' };
	var onMobile = true;

	var euler = new Euler(0, 0, 0, 'YXZ');

	var PI_2 = Math.PI / 2;

	var vec = new Vector3();

	var previousTouch;

	function onMouseMove(event) {

		var movementX, movementY;
		console.log('drar');
		if (scope.isLocked === false && onMobile === false) return;
		console.log('drar efter');

		if (event.type === 'touchmove') {
			const touch = event.touches[0];
			if (previousTouch) {
				// be aware that these only store the movement of the first touch in the touches array
				event.movementX = -(touch.pageX - previousTouch.pageX);
				event.movementY = -(touch.pageY - previousTouch.pageY);
			};

			previousTouch = touch;
		} 


			movementX = event.movementX || event.mozMovementX || event.webkitMovementX || 0;
			movementY = event.movementY || event.mozMovementY || event.webkitMovementY || 0;
        

		



		euler.setFromQuaternion(camera.quaternion);

        
		euler.y -= movementX * 0.002;
		euler.x -= movementY * 0.002;

		euler.x = Math.max(PI_2 - scope.maxPolarAngle, Math.min(PI_2 - scope.minPolarAngle, euler.x));

		camera.quaternion.setFromEuler(euler);

		scope.dispatchEvent(changeEvent);

	}

	function onPointerlockChange() {

		if (scope.domElement.ownerDocument.pointerLockElement === scope.domElement) {

			scope.dispatchEvent(lockEvent);

			scope.isLocked = true;

		}
		else {

			scope.dispatchEvent(unlockEvent);

			scope.isLocked = false;

		}

	}

	function onPointerlockError() {

		console.error('THREE.PointerLockControls: Unable to use Pointer Lock API');

	}

	this.connect = function () {

		scope.domElement.ownerDocument.addEventListener('mousemove', onMouseMove, false);
		scope.domElement.ownerDocument.addEventListener('touchmove', onMouseMove, false);
		scope.domElement.ownerDocument.addEventListener("touchend", (e) => {
			previousTouch = null;
		});
		scope.domElement.ownerDocument.addEventListener('pointerlockchange', onPointerlockChange, false);
		scope.domElement.ownerDocument.addEventListener('pointerlockerror', onPointerlockError, false);

	};

	this.disconnect = function () {

		scope.domElement.ownerDocument.removeEventListener('mousemove', onMouseMove, false);
		scope.domElement.ownerDocument.removeEventListener('pointerlockchange', onPointerlockChange, false);
		scope.domElement.ownerDocument.removeEventListener('pointerlockerror', onPointerlockError, false);

	};

	this.dispose = function () {

		this.disconnect();

	};

	this.getObject = function () { // retaining this method for backward compatibility

		return camera;

	};

	this.getDirection = function () {

		var direction = new Vector3(0, 0, - 1);

		return function (v) {

			return v.copy(direction).applyQuaternion(camera.quaternion);

		};

	}();

	this.moveForward = function (distance) {

		// move forward parallel to the xz-plane
		// assumes camera.up is y-up

		vec.setFromMatrixColumn(camera.matrix, 0);

		vec.crossVectors(camera.up, vec);

		camera.position.addScaledVector(vec, distance);

	};

	this.moveRight = function (distance) {

		vec.setFromMatrixColumn(camera.matrix, 0);

		camera.position.addScaledVector(vec, distance);

	};

	this.lock = function () {

		this.domElement.requestPointerLock();

	};

	this.unlock = function () {

		scope.domElement.ownerDocument.exitPointerLock();

	};

	/*this.unlockForArt = function(){

		scope.domElement.ownerDocument.exitPointerLock();
	}*/

	this.connect();

};

PointerLockControls.prototype = Object.create(EventDispatcher.prototype);
PointerLockControls.prototype.constructor = PointerLockControls;

export { PointerLockControls };