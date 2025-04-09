const imageInput = document.getElementById("imageUpload");
const previewImage = document.getElementById("previewImage");

imageInput.addEventListener("change", function () {
    if (this.files && this.files.length > 0) {
        const reader = new FileReader();
        reader.onload = (e) => {
            previewImage.src = e.target.result;
        };
        reader.readAsDataURL(this.files[0]);
    }
})