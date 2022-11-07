import React, {useEffect, useState} from 'react';
import {useDropzone} from 'react-dropzone';
import {Container, Stack} from "@mui/material";

const thumbsContainer = {
    display: 'flex',
    flexDirection: 'row',
    flexWrap: 'wrap',
    marginTop: 16
};

const thumb = {
    display: 'inline-flex',
    borderRadius: 2,
    border: '1px solid #eaeaea',
    marginBottom: 8,
    marginRight: 8,
    width: 100,
    height: 100,
    padding: 4,
    boxSizing: 'border-box'
};

const thumbInner = {
    display: 'flex',
    minWidth: 0,
    overflow: 'hidden'
};

const img = {
    display: 'block',
    width: 'auto',
    height: '100%'
};


export default function UploadDocument(props) {
    const {documentType, description, inputElementId, imageId} = props

    const [photoIDFile, setPhotoIDFile] = useState(null);
    const [imagePreview, setImagePreview] = useState("")
    
    const saveFile = (e) => {
        setPhotoIDFile(e.target.files[0]);
        setImagePreview(window.URL.createObjectURL(e.target.files[0]));
    }
    

    useEffect(() => {
        // Make sure to revoke the data uris to avoid memory leaks, will run on unmount
        return () => URL.revokeObjectURL(imagePreview);
    }, []);

    return (
       <> <h2>{documentType}</h2>
           <p>{description}</p>
           <input type="file" name="IdPhoto"
                  accept=".png,.jpg,.jpeg,.gif,.tif"
                  className="form-control border-dashed w-100"
                  id={inputElementId}
                  onChange={saveFile}
           />

           <aside style={thumbsContainer}>
               {imagePreview ?  <img id={imageId} className="img-fluid my-3 dropzone d-block-inline" width="300" height="300"
                                     src={imagePreview} alt="Id Photo"/> : <div></div>}
           </aside>
       </>
        
    
    );
}
