(function () {
    const dropZone = document.getElementById('dropZone');
    const fileInput = document.getElementById('fileInput');
    const message = document.getElementById('uploadValidationMessage');
    const allowedExtensions = ['.pdf', '.jpg', '.png'];
    const maxFileSizeBytes = 20 * 1024 * 1024;

    if (!dropZone || !fileInput) {
        return;
    }

    const validate = (files) => {
        const errors = [];

        [...files].forEach(file => {
            const extension = `.${file.name.split('.').pop().toLowerCase()}`;
            if (!allowedExtensions.includes(extension)) {
                errors.push(`${file.name}: invalid file type.`);
            }
            if (file.size > maxFileSizeBytes) {
                errors.push(`${file.name}: exceeds max size (20MB).`);
            }
        });

        message.textContent = errors.join(' ');
        return errors.length === 0;
    };

    dropZone.addEventListener('dragover', (event) => {
        event.preventDefault();
        dropZone.classList.add('drag-over');
    });

    dropZone.addEventListener('dragleave', () => {
        dropZone.classList.remove('drag-over');
    });

    dropZone.addEventListener('drop', (event) => {
        event.preventDefault();
        dropZone.classList.remove('drag-over');
        if (event.dataTransfer.files.length > 0) {
            fileInput.files = event.dataTransfer.files;
            validate(fileInput.files);
        }
    });

    fileInput.addEventListener('change', () => validate(fileInput.files));
})();
