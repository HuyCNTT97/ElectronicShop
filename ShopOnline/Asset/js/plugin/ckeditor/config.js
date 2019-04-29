/// <reference path="../ckfinder/ckfinder.js" />
/**
 * @license Copyright (c) 2003-2018, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E'
   
    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
    config.language = 'vi';
    config.filebrowserBrowseUrl = '/Asset/js/plugin/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Asset/js/plugin/ckfinder/ckfinder.html?type=Images';
    config.filebrowserFlashBrowseUrl = '/Asset/js/plugin/ckfinder/ckfinder.html?type=Flash';
    config.filebrowserUploadUrl = '/Asset/js/plugin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/Data';
    config.filebrowserFlashUploadUrl = '/Asset/js/plugin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
    CKFinder.setupCKEditor(null, '/Asset/js/plugin/ckfinder/'); 
};
