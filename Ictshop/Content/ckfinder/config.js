/*
Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://cksource.com/ckfinder/license
*/

CKFinder.customConfig = function( config )
{
	// Define changes to default configuration here.
	// For the list of available options, check:
	// http://docs.cksource.com/ckfinder_2.x_api/symbols/CKFinder.config.html

	// Sample configuration options:
	// config.uiColor = '#BDE31E';
	// config.language = 'fr';
	// config.removePlugins = 'basket';



};
CKEDITOR.editorConfig = function (config) {
	config.filebrowserImageUploadUrl = '/ckfinder/core/connector/php/connector.php?command=QuickUpload&type=Images';
	config.image_prefillDimensions = false; // Không ép kích thước ảnh
	config.image_maxWidth = 1000;  // Giới hạn chiều rộng ảnh
	config.image_maxHeight = 1000; // Giới hạn chiều cao ảnh
	config.quality = 90;
};