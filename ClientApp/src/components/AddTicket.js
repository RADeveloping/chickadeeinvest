import {usePost} from "./FetchData";
import {useEffect, useState} from "react";
import {Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, TextField} from "@mui/material";

export default function AddTicket({propertyId, unitId, open, handleClose}) {
    const [ticket, setTicket] = useState(null);
    const [resp, error, loading] = usePost(`/api/properties/${propertyId}/units/${unitId}/tickets`, ticket)
    
    const handleAdd = () => {
        setTicket({
            "status": 0,
            "description": "HEY"
        })
    }
    
    // useEffect(()=>{
    //     if (resp) {
    //         console.log(resp)
    //     }
    // }, [resp])
    
    return (
        <Dialog open={open} onClose={handleClose}>
            <DialogTitle>Subscribe</DialogTitle>
            <DialogContent>
                <DialogContentText>
                    To subscribe to this website, please enter your email address here. We
                    will send updates occasionally.
                </DialogContentText>
                <TextField
                    autoFocus
                    margin="dense"
                    id="name"
                    label="Email Address"
                    type="email"
                    fullWidth
                    variant="standard"
                />
            </DialogContent>
            <DialogActions>
                <Button onClick={handleClose}>Cancel</Button>
                <Button onClick={handleAdd}>Subscribe</Button>
            </DialogActions>
        </Dialog>
    )
}