import Logo from "./Logo";
import {CircularProgress, Grow} from "@mui/material";

export default function LogoLoading() {
    return <Grow in={true}>
        <div style={{
        height: '100%',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
    }}>
            <div style={{position: 'absolute'}}>
        <CircularProgress size={180} thickness={2} />
            </div>
            <div style={{position: 'absolute'}}>
                <Logo sx={{width: 100, height: 100}}/>
            </div>
    </div>
    </Grow>
}