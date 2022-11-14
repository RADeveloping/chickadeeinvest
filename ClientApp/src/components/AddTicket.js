import useFetch, {usePost} from "./FetchData";
import {useEffect, useState} from "react";
import {
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogContentText,
    DialogTitle, FormControl, InputLabel, MenuItem,
    Select, Stack,
    TextField
} from "@mui/material";
import * as React from "react";
import Label from "./Label";
import {getTicketsUri, SEVERITY} from "../utils/filters";
import {useNavigate} from "react-router-dom";
import {LoadingButton} from "@mui/lab";

export default function AddTicket({propertyId, unitId, open, handleClose}) {
    const [problem, setProblem] = useState('');
    const [description, setDescription] = useState('');
    const [severity, setSeverity] = useState(0);
    const [ticket, setTicket] = useState(null);
    const [properties, errorProperties, loadingProperties] = useFetch('/api/properties/');
    const [units, errorUnits, loadingUnits] = useFetch('/api/units/');
    if (!propertyId) propertyId = properties.length > 0 ? properties[0].propertyId : null;
    if (!unitId) unitId = units.length > 0 ? units[0].unitId : null;
    const [resp, error, loading] = usePost(propertyId && unitId ? `/api/properties/${propertyId}/units/${unitId}/tickets` : null, ticket)
    const navigate = useNavigate();

    const handleAdd = () => {
        setTicket({
            "severity": severity,
            "description": description,
            "problem": problem
        })
    }

    const handleSeverityChange = (event) => {
        setSeverity(event.target.value);
    };

    useEffect(() => {
        if (resp.ticketId) {
            navigate("/dashboard/" + getTicketsUri(resp))
        }
    }, [resp])

    return (
        <Dialog fullWidth open={open} onClose={handleClose}>
            <DialogTitle>Add Ticket</DialogTitle>
            <DialogContent>
                <Stack direction={'column'}>
                    <TextField
                        autoFocus
                        margin="dense"
                        label="Problem"
                        fullWidth
                        value={problem}
                        variant="standard"
                        onChange={(e) => setProblem(e.target.value)}
                    />
                    <TextField
                        multiline
                        margin="dense"
                        label="Description"
                        fullWidth
                        value={description}
                        variant="standard"
                        onChange={(e) => setDescription(e.target.value)}
                    />
                    <br/>
                    <FormControl>
                        <InputLabel id="severity">Severity</InputLabel>
                        <Select
                            value={severity}
                            labelId="severity"
                            label="Severity"
                            onChange={handleSeverityChange}
                        >
                            {Object.keys(SEVERITY).map((key) =>
                                <MenuItem key={key} value={key}>
                                    <Label
                                        variant="ghost"
                                        color={SEVERITY[key].color}
                                    >
                                        {SEVERITY[key].text}
                                    </Label>
                                </MenuItem>
                            )}
                        </Select>
                    </FormControl>
                </Stack>
            </DialogContent>
            <DialogActions>
                <Button onClick={handleClose}>Cancel</Button>
                <LoadingButton loading={loading} disabled={description === '' || problem === '' || resp.ticketId}
                               onClick={handleAdd}>{resp.ticketId ? 'Added' : 'Add'}</LoadingButton>
            </DialogActions>
        </Dialog>
    )
}