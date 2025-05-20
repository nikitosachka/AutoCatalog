function previewImage(event, id) {
    var reader = new FileReader();
    reader.onload = function () {
        var output = document.getElementById('preview-' + id);
        output.src = reader.result;
        output.style.display = 'block';
    }
    reader.readAsDataURL(event.target.files[0]);
}
