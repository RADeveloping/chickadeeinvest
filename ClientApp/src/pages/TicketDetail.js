import {Link as RouterLink, useNavigate, useParams} from "react-router-dom";
import {Button, Container, Stack, Typography} from "@mui/material";
import * as React from "react";
import Page from "../components/Page";
import Iconify from "../components/Iconify";

export default function TicketDetail() {
    const title = "Ticket"
    const { id } = useParams();
    const navigate = useNavigate();
    return (
        <Page title={`${title} #${id}`}>
            <Container>
                <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Button
                        variant="contained"
                        startIcon={<Iconify icon="eva:plus-fill" />}
                        onClick={()=>navigate(-1)}
                    >
                        Back
                    </Button>
                    <Typography variant="h4" gutterBottom>
                        {title} #{id}
                    </Typography>
                </Stack>
            </Container>
        </Page>
    )
}