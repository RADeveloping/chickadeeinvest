import {Box, CircularProgress} from "@mui/material";
import * as React from "react";

/**
 * Component for page loading.
 * @param loadingData {boolean} Loading state.
 * @returns {JSX.Element|null}
 * @constructor
 */
export default function PageLoading({loadingData}) {
    return (
        loadingData ?
            <Box display="flex"
                 justifyContent="center"
                 alignItems="center"
                 height="50vh">
                <CircularProgress/>
            </Box> : null
    )
}