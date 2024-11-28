
  (() => {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    const forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.from(forms).forEach(form => {
      form.addEventListener('submit', event => {
        let isValid = true;
        Array.from(form.elements).forEach(field => {
          if (field.checkValidity() === false) {
            isValid = false;
            const errorMessage = field.parentNode.querySelector('.invalid-feedback');
            if (errorMessage) {
              errorMessage.style.display = 'block';
            }
          }
        });

        if (!isValid) {
          event.preventDefault();
          event.stopPropagation();
        }

        // form.classList.add('was-validated');
      }, false);

      // Add event listener to hide error message on input
      Array.from(form.elements).forEach(field => {
        field.addEventListener('input', function() {
          const errorMessage = this.parentNode.querySelector('.invalid-feedback');
          if (errorMessage) {
            errorMessage.style.display = 'none';
          }
        });
      });
    });
  })()
