jQueryAjaxUpdateBio = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (data) {
                $(`#bio`).text(data);
                $("#editBioModal .close").click()
            },
            error: function (err) {
                console.log(err)
            }
        })
    } catch (ex) {
        console.log(ex)
    }
    return false;
}

jQueryAjaxUpdateFacebook = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (data) {
               
                $("#editFacebookModal .close").click()
            },
            error: function (err) {
                console.log(err)
            }
        })
    } catch (ex) {
        console.log(ex)
    }
    return false;
}

jQueryAjaxUpdateTwitter = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (data) {

                $("#editTwitterModal .close").click()
            },
            error: function (err) {
                console.log(err)
            }
        })
    } catch (ex) {
        console.log(ex)
    }
    return false;
}

jQueryAjaxUpdateInstagram = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (data) {

                $("#editInstagramModal .close").click()
            },
            error: function (err) {
                console.log(err)
            }
        })
    } catch (ex) {
        console.log(ex)
    }
    return false;
}

jQueryAjaxDelete = form => {
    if (confirm('Do you want to delete this artwork?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (data) {
                    $(`#${data}`).remove();
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
                    $('#type-art-exhibit').append(`<div class="exhibit-art" ><picture>` + `<img src=/imagesArt/${data} id="image-exhibition"> `) 
                       
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



const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

connection.onclose(start);

start();




document.getElementById("sendMessageBtn").addEventListener('click', async function () {
    const message = document.getElementById("message").value;
    const name = document.getElementById("name").value;
    try {
        await connection.invoke("SendMessage", name, message);
    } catch (err) {
        console.log(err);
    }
})

connection.on("ReceiveMessage", (name, message) => {
    const rec_msg = name + ": " + message;
    const li = document.createElement("li");
    li.textContent = rec_msg;
    document.getElementById("messageList").appendChild(li);
})

$('#editBioModal').on('shown.bs.modal', function () {
    $('#myInput').trigger('focus')
})

