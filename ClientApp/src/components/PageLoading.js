import {Box, CircularProgress} from "@mui/material";
import * as React from "react";

export default function PageLoading({loadingData}) {
    return (
        loadingData ?
                <Box   display="flex"
                       justifyContent="center"
                       alignItems="center"
                       height="50vh">
                    <CircularProgress />
                </Box> : null 
    )
}