jQueryAjaxDelete = form => {
    if (confirm('Are you sure to delete this artwork?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (data) {
                    $(`#removeMe-${data}`).remove();
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }

    //prevent default form submit event
    return false;
}
jQueryAjaxUpdate = form => {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (data) {
                    $('.artwork-container').append('<div class="exhibit-art"><picture>' + `<img src=/imagesArt/${data}>`); 
                    //`<form asp-action="DeleteArt" asp-route-id="@item.ArtworkId" onsubmit="return jQueryAjaxDelete(this);" class="d-inline"` +
                    //`<input type="submit" value="Delete" class="btn btn-danger">`+
                    //`</form>`);
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
    }
    //prevent default form submit event
    return false;
}


document.querySelectorAll(".drop-zone__input").forEach((inputElement) => {
    const dropZoneElement = inputElement.closest(".drop-zone");

    dropZoneElement.addEventListener("click", (e) => {
        inputElement.click();
    });

    inputElement.addEventListener("change", (e) => {
        if (inputElement.files.length) {
            updateThumbnail(dropZoneElement, inputElement.files[0]);
        }
    });

    dropZoneElement.addEventListener("dragover", (e) => {
        e.preventDefault();
        dropZoneElement.classList.add("drop-zone--over");
    });

    ["dragleave", "dragend"].forEach((type) => {
        dropZoneElement.addEventListener(type, (e) => {
            dropZoneElement.classList.remove("drop-zone--over");
        });
    });

    dropZoneElement.addEventListener("drop", (e) => {
        e.preventDefault();

        if (e.dataTransfer.files.length) {
            inputElement.files = e.dataTransfer.files;
            updateThumbnail(dropZoneElement, e.dataTransfer.files[0]);
        }

        dropZoneElement.classList.remove("drop-zone--over");
    });
});

function updateThumbnail(dropZoneElement, file) {
    let thumbnailElement = dropZoneElement.querySelector(".drop-zone__thumb");

    // First time - remove the prompt
    if (dropZoneElement.querySelector(".drop-zone__prompt")) {
        dropZoneElement.querySelector(".drop-zone__prompt").remove();
    }

    // First time - there is no thumbnail element, so lets create it
    if (!thumbnailElement) {
        thumbnailElement = document.createElement("div");
        thumbnailElement.classList.add("drop-zone__thumb");
        dropZoneElement.appendChild(thumbnailElement);
        thumbnailElement.setAttribute("asp-for", "@Model.Artwork.ImageFile");
    }

    thumbnailElement.dataset.label = file.name;

    // Show thumbnail for image files
    if (file.type.startsWith("image/")) {
        const reader = new FileReader();

        reader.readAsDataURL(file);
        reader.onload = () => {
            thumbnailElement.style.backgroundImage = `url('${reader.result}')`;
        };
    } else {
        thumbnailElement.style.backgroundImage = null;
    }
}


//function dragOverHandler(ev) {
//    console.log('File(s) in drop zone');

//    // Prevent default behavior (Prevent file from being opened)
//    ev.preventDefault();
//}

//let listOfObjects = [];
//function dropHandler(ev) {
//    console.log('File(s) dropped');
//    // Prevent default behavior (Prevent file from being opened)
//    ev.preventDefault();

//    if (ev.dataTransfer.items) {
//        // Use DataTransferItemList interface to access the file(s)
//        for (var i = 0; i < ev.dataTransfer.items.length; i++) {
//            // If dropped items aren't files, reject them
//            if (ev.dataTransfer.items[i].kind === 'file' && ev.dataTransfer.items[i].type == 'image/jpeg' || ev.dataTransfer.items[i].type == 'image/png') {
//                var file = ev.dataTransfer.items[i].getAsFile();
//                console.log('... file[' + i + '].name = ' + file.name);
//                listOfObjects.push(file);
//                console.log(listOfObjects)
//            }
//        }
//    } else {
//        // Use DataTransfer interface to access the file(s)
//        for (var i = 0; i < ev.dataTransfer.files.length; i++) {
//            console.log('... file[' + i + '].name = ' + ev.dataTransfer.files[i].name);
//        }
//    }

//    // Pass event to removeDragData for cleanup
//    removeDragData(ev)
//}

//function removeDragData(ev) {
//    console.log('Removing drag data')

//    if (ev.dataTransfer.items) {
//        // Use DataTransferItemList interface to remove the drag data
//        ev.dataTransfer.items.clear();
//    } else {
//        // Use DataTransfer interface to remove the drag data
//        ev.dataTransfer.clearData();
//    }
//}
