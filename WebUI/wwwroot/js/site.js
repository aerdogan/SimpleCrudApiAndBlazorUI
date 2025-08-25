window.closeBootstrapModal = (modalId) => {
    var modalEl = document.querySelector(modalId);
    var modal = bootstrap.Modal.getInstance(modalEl);
    if (modal) {
        modal.hide();
    }
};