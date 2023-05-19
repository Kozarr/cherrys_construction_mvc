
// Icon Picker
var myIconPicker = new UniversalIconPicker('#icon-picker', {
    iconLibraries: [
        'font-awesome.min.json',
    ],
    iconLibrariesCss: [
        // from CDN
        'https://cdnjs.cloudflare.com/ajax/libs/bootstrap-icons/1.8.1/font/bootstrap-icons.min.css',
        'https://kit.fontawesome.com/8a1d5615a6.js',
    ],
});

var myIconPicker = new UniversalIconPicker('#icon-picker', {
    resetSelector: '#clear-icon-picker',
    onReset: function () {
        //  do something
    }
});

var myIconPicker = new UniversalIconPicker('#icon-picker', {
    onSelect: function (jsonIconData) {
        // jsonIconData.libraryId
        // jsonIconData.libraryName
        // jsonIconData.iconHtml
        // jsonIconData.iconMarkup
        // jsonIconData.iconClass
        // jsonIconData.iconText
    },
});
