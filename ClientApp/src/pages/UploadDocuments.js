import React, {useEffect, useState} from 'react';
import UploadDocument from "../components/UploadDocument";
import {Divider, Stack} from "@mui/material";
import useFetch from "../components/FetchData";

export default function UploadDocuments(props) {
    const {selectedUnitIdParent} = props
    
    // useEffect(() => {
    //     if(documents && !loadingDocuments) {
    //         console.log(documents)
    //     }
    // }, []);
    //
    
    return (
        <Stack
            direction="column"
            divider={<Divider orientation="horizontal" flexItem />}
            spacing={2}
        >
            <UploadDocument flexItem documentType={"Photo ID"}  description={"Please upload a picture of your Identification."}
                            inputElementId={"input.IdPhoto"} imageId={"IdPhoto"} selectedUnitIdParent={selectedUnitIdParent}
                            
            />


            <UploadDocument flexItem documentType={"Lease Agreement"}  description={"Please upload a picture of your Lease Agreement."}
                            inputElementId={"Input.LeasePhoto"} imageId={"leasePhoto"} selectedUnitIdParent={selectedUnitIdParent}
                            
            />
        </Stack>
              

);
}
