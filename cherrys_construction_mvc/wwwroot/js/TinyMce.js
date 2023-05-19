tinymce.init({
    selector: 'textarea#readonly',
    readonly: true
});

tinymce.init({
    selector: 'textarea#tinyMceMail',
    height: 270,
});

tinymce.init({
    selector: 'textarea',
    plugins: 'anchor autolink charmap codesample emoticons link lists searchreplace visualblocks wordcount',
    toolbar: 'undo redo | blocks fontsize | bold italic underline strikethrough | align lineheight | numlist bullist indent outdent | emoticons charmap | removeformat',
});



