import PropTypes from 'prop-types';
import {useEffect} from 'react';
import {useLocation} from 'react-router-dom';
// material
import {styled} from '@mui/material/styles';
import {Box, Drawer, Fade, Grow} from '@mui/material';
// components
import Logo from '../../components/common/Logo';
import Scrollbar from '../../components/nav/Scrollbar';
import {useTheme} from '@emotion/react';
import useResponsive from "../../utils/responsive";
import NavSection from "../../components/nav/NavSection";
import navConfig, {anonymousNavConfig} from "./NavConfig";

// ----------------------------------------------------------------------

const DRAWER_WIDTH = 280;

const RootStyle = styled('div')(({theme}) => ({
    [theme.breakpoints.up('lg')]: {
        flexShrink: 0,
        width: DRAWER_WIDTH,
    },
}));

const AccountStyle = styled('div')(({theme}) => ({
    display: 'flex',
    alignItems: 'center',
    padding: theme.spacing(2, 2.5),
    borderRadius: Number(theme.shape.borderRadius) * 1.5,
    backgroundColor: theme.palette.grey[500_12],
}));

// ----------------------------------------------------------------------

DashboardSidebar.propTypes = {
    isOpenSidebar: PropTypes.bool,
    onCloseSidebar: PropTypes.func,
};

export default function DashboardSidebar({authenticated, isOpenSidebar, onCloseSidebar}) {
    const {pathname} = useLocation();

    const isDesktop = useResponsive('up', 'lg');

    useEffect(() => {
        if (isOpenSidebar) {
            onCloseSidebar();
        }
    }, [pathname]);

    const theme = useTheme();

    const renderContent = (theme) => (
        <Scrollbar
            sx={{
                backgroundColor: theme.palette.primary.main,
                height: 1,
                '& .simplebar-content': {height: 1, display: 'flex', flexDirection: 'column'},
            }}
        >
            <Box sx={{px: 2.5, py: 3, display: 'inline-flex'}}>
                <Logo/>
            </Box>

            <Box sx={{px: 2.5, py: 5, display: 'inline-flex'}}></Box>
            <Grow timeout={!isDesktop ? 0 : undefined} in={authenticated !== null}>
                <div>
                    <NavSection navConfig={authenticated === true ? navConfig : anonymousNavConfig}/>
                </div>
            </Grow>
            <Box sx={{flexGrow: 1}}/>
        </Scrollbar>
    );

    return (
        <RootStyle>
            {!isDesktop && (
                <Drawer
                    open={isOpenSidebar}
                    onClose={onCloseSidebar}
                    PaperProps={{
                        sx: {width: DRAWER_WIDTH},
                    }}
                >
                    {renderContent(theme)}
                </Drawer>
            )}

            {isDesktop && (
                <Drawer
                    open
                    variant="persistent"
                    PaperProps={{
                        sx: {
                            width: DRAWER_WIDTH,
                            bgcolor: 'background.default',
                            borderRightStyle: 'dashed',
                        },
                    }}
                >
                    {renderContent(theme)}
                </Drawer>
            )}
        </RootStyle>
    );
}
