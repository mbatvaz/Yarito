document.addEventListener('DOMContentLoaded', function() {
    const profileInput = document.getElementById('profileImage');
    const avatarImage = document.getElementById('avatarImage');
    const deleteInput = document.getElementById('DeleteProfileImage');
    const removeBtn = document.getElementById('removeProfileImageBtn');
    const defaultImageSrc = '/Images/Profile/default.png';

    // Handle File Selection
    if (profileInput) {
        profileInput.addEventListener('change', function (e) {
            const file = e.target.files[0];
            if (file) {
                const reader = new FileReader();
                reader.onload = function (e) {
                    if (avatarImage) {
                        avatarImage.src = e.target.result;
                    }
                };
                reader.readAsDataURL(file);
                
                // Reset delete flag if user uploads a new image
                if (deleteInput) {
                    deleteInput.value = "false";
                }
            }
        });
    }

    // Handle Remove Button
    if (removeBtn) {
        removeBtn.addEventListener('click', function() {
            // 1. Reset Image to Default
            if (avatarImage) {
                avatarImage.src = defaultImageSrc;
            }

            // 2. Clear Input
            if (profileInput) {
                profileInput.value = '';
            }

            // 3. Set Delete Flag
            if (deleteInput) {
                deleteInput.value = "true";
            }
        });
    }
});
