import {useEffect, useState} from "react";
import {
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle, FormControl, InputLabel, MenuItem,
    Select, Stack,
    TextField
} from "@mui/material";
import * as React from "react";
import {useNavigate} from "react-router-dom";
import {LoadingButton} from "@mui/lab";
import useFetch, {usePost} from "../../utils/fetch";
import {getTicketsUri, SEVERITY} from "../../utils/constants";
import Label from "../common/Label";


/**
 * Add ticket overlay component.
 * @param title {string} String displayed in the title.
 * @param propertyId {string} Property Id.
 * @param unitId {string} Unit Id.
 * @param open {boolean} Overlay open state.
 * @param handleClose Handles close for the open state.
 * @returns {JSX.Element}
 * @constructor
 */
export default function AddTicket({title, propertyId, unitId, open, handleClose}) {
    const [problem, setProblem] = useState('');
    const [description, setDescription] = useState('');
    const [severity, setSeverity] = useState(0);
    const [ticket, setTicket] = useState(null);
    const [properties, errorProperties, loadingProperties] = useFetch('/api/properties/');
    const [units, errorUnits, loadingUnits] = useFetch('/api/units/');
    if (!propertyId) propertyId = properties.length > 0 ? properties[0].propertyId : null;
    if (!unitId) unitId = units.length > 0 ? units[0].unitId : null;
    if (!title) title = units.length > 0 ? units[0].unitNo : null;
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
            <DialogTitle>Add Ticket {title ? `to Unit ${title}` : null}</DialogTitle>
            <DialogContent>
                <Stack direction={'column'}>
                    <Stack direction={'row'} alignItems={'center'}>
                        <TextField
                            fullWidth
                            autoFocus
                            margin="dense"
                            label="Problem"
                            value={problem}
                            variant="standard"
                            onChange={(e) => setProblem(e.target.value)}
                        />
                        <FormControl sx={{minWidth: 90, marginBottom: '5px'}} margin="dense" variant="standard">
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
                                            key={key}
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

                    <TextField
                        multiline
                        margin="dense"
                        label="Description"
                        fullWidth
                        value={description}
                        variant="standard"
                        onChange={(e) => setDescription(e.target.value)}
                    />

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