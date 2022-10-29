import { Link as RouterLink } from 'react-router-dom';
// @mui
import { styled } from '@mui/material/styles';
import { Card, Link, Container, Typography } from '@mui/material';
// hooks
import useResponsive from '../hooks/useResponsive';
// components
import Page from '../components/Page';
import Logo from '../components/Logo';
// sections
import AuthSocial from '../sections/auth/AuthSocial';
import ProfileForm from "../components/ProfileForm";

// ----------------------------------------------------------------------
const ContentStyle = styled('div')(({ theme }) => ({
    minHeight: '100vh',
    display: 'flex',
    flexDirection: 'column',
    padding: theme.spacing(0, 0),
}));

// ----------------------------------------------------------------------

export default function Profile() {
    const smUp = useResponsive('up', 'sm');
    const mdUp = useResponsive('up', 'md');
    
    return (
        <Page title="Profile">
                <Container>
                    <ContentStyle>
                        <Typography variant="h4" gutterBottom>
                            Personal info
                        </Typography>
                        <ProfileForm />
                    </ContentStyle>
                </Container>
        </Page>
    );
}
