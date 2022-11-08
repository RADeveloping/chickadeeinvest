import * as React from 'react';
import PropTypes from 'prop-types';
import { styled } from '@mui/material/styles';
import Stack from '@mui/material/Stack';
import Stepper from '@mui/material/Stepper';
import Step from '@mui/material/Step';
import StepLabel from '@mui/material/StepLabel';
import Check from '@mui/icons-material/Check';
import SettingsIcon from '@mui/icons-material/Settings';
import GroupAddIcon from '@mui/icons-material/GroupAdd';
import VideoLabelIcon from '@mui/icons-material/VideoLabel';
import StepConnector, { stepConnectorClasses } from '@mui/material/StepConnector';
import {Box, Button, Container, Paper, Typography} from "@mui/material";
import {AccountCircle, Copyright, Home, Verified} from "@mui/icons-material";
import SelectPropertyOverview from "../components/SelectPropertyOverview";
import {useEffect} from "react";
import UploadDocuments from "./UploadDocuments";

const QontoConnector = styled(StepConnector)(({ theme }) => ({
    [`&.${stepConnectorClasses.alternativeLabel}`]: {
        top: 10,
        left: 'calc(-50% + 16px)',
        right: 'calc(50% + 16px)',
    },
    [`&.${stepConnectorClasses.active}`]: {
        [`& .${stepConnectorClasses.line}`]: {
            borderColor: '#784af4',
        },
    },
    [`&.${stepConnectorClasses.completed}`]: {
        [`& .${stepConnectorClasses.line}`]: {
            borderColor: '#784af4',
        },
    },
    [`& .${stepConnectorClasses.line}`]: {
        borderColor: theme.palette.mode === 'dark' ? theme.palette.grey[800] : '#eaeaf0',
        borderTopWidth: 3,
        borderRadius: 1,
    },
}));

const QontoStepIconRoot = styled('div')(({ theme, ownerState }) => ({
    color: theme.palette.mode === 'dark' ? theme.palette.grey[700] : '#eaeaf0',
    display: 'flex',
    height: 22,
    alignItems: 'center',
    ...(ownerState.active && {
        color: '#784af4',
    }),
    '& .QontoStepIcon-completedIcon': {
        color: '#784af4',
        zIndex: 1,
        fontSize: 18,
    },
    '& .QontoStepIcon-circle': {
        width: 8,
        height: 8,
        borderRadius: '50%',
        backgroundColor: 'currentColor',
    },
}));

function QontoStepIcon(props) {
    const { active, completed, className } = props;

    return (
        <QontoStepIconRoot ownerState={{ active }} className={className}>
            {completed ? (
                <Check className="QontoStepIcon-completedIcon" />
            ) : (
                <div className="QontoStepIcon-circle" />
            )}
        </QontoStepIconRoot>
    );
}

QontoStepIcon.propTypes = {
    /**
     * Whether this step is active.
     * @default false
     */
    active: PropTypes.bool,
    className: PropTypes.string,
    /**
     * Mark the step as completed. Is passed to child components.
     * @default false
     */
    completed: PropTypes.bool,
};

const ColorlibConnector = styled(StepConnector)(({ theme }) => ({
    [`&.${stepConnectorClasses.alternativeLabel}`]: {
        top: 22,
    },
    [`&.${stepConnectorClasses.active}`]: {
        [`& .${stepConnectorClasses.line}`]: {
            backgroundImage:
                'linear-gradient( 95deg,rgb(242,113,33) 0%,rgb(233,64,87) 50%,rgb(138,35,135) 100%)',
        },
    },
    [`&.${stepConnectorClasses.completed}`]: {
        [`& .${stepConnectorClasses.line}`]: {
            backgroundImage:
                'linear-gradient( 95deg,rgb(242,113,33) 0%,rgb(233,64,87) 50%,rgb(138,35,135) 100%)',
        },
    },
    [`& .${stepConnectorClasses.line}`]: {
        height: 3,
        border: 0,
        backgroundColor:
            theme.palette.mode === 'dark' ? theme.palette.grey[800] : '#eaeaf0',
        borderRadius: 1,
    },
}));

const ColorlibStepIconRoot = styled('div')(({ theme, ownerState }) => ({
    backgroundColor: theme.palette.mode === 'dark' ? theme.palette.grey[700] : '#ccc',
    zIndex: 1,
    color: '#fff',
    width: 50,
    height: 50,
    display: 'flex',
    borderRadius: '50%',
    justifyContent: 'center',
    alignItems: 'center',
    ...(ownerState.active && {
        background: '#103A31',
        boxShadow: '0 4px 10px 0 rgba(0,0,0,.25)',
    }),
    ...(ownerState.completed && {
        background: '#103A31',
    }),
}));

function ColorlibStepIcon(props) {
    const { active, completed, className } = props;

    const icons = {
        1: <AccountCircle />,
        2: <Home />,
        3: <Verified />,
    };

    return (
        <ColorlibStepIconRoot ownerState={{ completed, active }} className={className}>
            {icons[String(props.icon)]}
        </ColorlibStepIconRoot>
    );
}

ColorlibStepIcon.propTypes = {
    /**
     * Whether this step is active.
     * @default false
     */
    active: PropTypes.bool,
    className: PropTypes.string,
    /**
     * Mark the step as completed. Is passed to child components.
     * @default false
     */
    completed: PropTypes.bool,
    /**
     * The label displayed in the step icon.
     */
    icon: PropTypes.node,
};

const steps = ['Register', 'Select Unit', 'Verify Documents'];

function getStepContent(step, nextButtonEnabled, setNextButtonEnabled,selectedUnitIdParent, setSelectedUnitIdParent, 
                        setPhotoId, setLeaseAgreement) {
    switch (step) {
        case 0:
        case 1:
            return <SelectPropertyOverview nextButtonEnabled={nextButtonEnabled} setNextButtonEnabled={setNextButtonEnabled}
                                           selectedUnitIdParent={selectedUnitIdParent} setSelectedUnitIdParent={setSelectedUnitIdParent}
            />;
        case 2:
            return <UploadDocuments  setPhotoId = {setPhotoId} setLeaseAgreement = {setLeaseAgreement}
                                     selectedUnitIdParent={selectedUnitIdParent} setSelectedUnitIdParent={setSelectedUnitIdParent}

            />;
        default:
            throw new Error('Unknown step');
    }
}

export default function Verification() {
    const [activeStep, setActiveStep] = React.useState(1);
    const [nextButtonEnabled, setNextButtonEnabled] = React.useState(false);
    const [selectedUnitIdParent, setSelectedUnitIdParent] = React.useState(null);
    const [photoId, setPhotoId] = React.useState(null);
    const [leaseAgreement, setLeaseAgreement] = React.useState(null);

    useEffect(() => {
        if (selectedUnitIdParent == null) {
            setNextButtonEnabled(false);
        }else{
            setNextButtonEnabled(true);
        }
        console.log(selectedUnitIdParent)

    }, [selectedUnitIdParent])


    useEffect(() => {
   
        console.log(photoId)

    }, [photoId])

    
    const handleNext = () => {
        if(activeStep === 2){
            console.log("SUBMIT")
        }
        setActiveStep(activeStep + 1);
    };

    const handleBack = () => {
        if(activeStep - 1 !== 0){
            setActiveStep(activeStep - 1);
        }
    };

    
    return (
        <Container component="main" maxWidth="md" sx={{ mb: 4 }}>
            <Paper variant="outlined" sx={{ my: { xs: 3, md: 6 }, p: { xs: 2, md: 3 } }}>
                <Typography component="h1" variant="h4" align="center">
                    Getting Started
                </Typography>
                 <Stack sx={{ width: '100%', marginY: '50px'}} spacing={4}>
                     <Stepper alternativeLabel activeStep={activeStep} connector={<ColorlibConnector />}>
                         {steps.map((label) => (
                             <Step key={label}>
                                 <StepLabel StepIconComponent={ColorlibStepIcon}>{label}</StepLabel>
                             </Step>
                         ))}
                     </Stepper>
                 </Stack>
                {activeStep === steps.length ? (
                    <React.Fragment>
                        <Typography variant="h5" gutterBottom>
                            Thank you for submitting your details.
                        </Typography>
                        <Typography variant="subtitle1">
                            Your account is pending verification. We will email you an update when your account has been verified.
                        </Typography>
                    </React.Fragment>
                ) : (
                    <React.Fragment>
                        {getStepContent(activeStep, nextButtonEnabled, setNextButtonEnabled, selectedUnitIdParent, setSelectedUnitIdParent,
                            photoId, setPhotoId, leaseAgreement, setLeaseAgreement)}
                        <Box sx={{ display: 'flex', justifyContent: 'flex-end' }}>
                            {activeStep !== 1 && (
                                <Button onClick={handleBack} sx={{ mt: 3, ml: 1 }}>
                                    Back
                                </Button>
                            )}
                            
                            <Button
                                variant="contained"
                                onClick={handleNext}
                                sx={{ mt: 3, ml: 1}}
                                disabled={!nextButtonEnabled}
                            >
                                {activeStep === steps.length - 1 ? 'Submit' : 'Next'}
                            </Button>
                        </Box>
                    </React.Fragment>
                )}
            </Paper>
            <Copyright />
        </Container>
        
        
    );
}
