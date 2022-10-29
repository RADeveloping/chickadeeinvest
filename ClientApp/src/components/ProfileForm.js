import * as Yup from 'yup';
import {useEffect, useState} from 'react';
import { useNavigate } from 'react-router-dom';
// form
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
// @mui
import { Stack, IconButton, InputAdornment } from '@mui/material';
import { LoadingButton } from '@mui/lab';
// components
import Iconify from '../components/Iconify';
import { FormProvider, RHFTextField } from '../components/hook-form';

// ----------------------------------------------------------------------

export default function ProfileForm() {
  const navigate = useNavigate();
  
    const [user, setUser] = useState({
        firstName: "",
        lastName: "",
        username: "",
        emailAddress: "",
        phoneNumber: "",
    });

    // fetch user data in use effect
    useEffect(() => {
        fetch("/api/Account/")
            .then((response) => response.json())
            .then((data) => {
                setUser(data);

            });
    }, [user]);


    const ProfileSchema = Yup.object().shape({
    firstName: Yup.string().required('First name required'),
    lastName: Yup.string().required('Last name required'),
    username: Yup.string().email('Username must be a valid email address').required('Username is required'),
    email: Yup.string().email('Email must be a valid email address').required('Email is required'),
    phoneNumber: Yup.string().nullable().optional(),
  });

    function defaultValues() {
        return {
            firstName: user.firstName,
            lastName: user.lastName,
            username: user.username,
            email: user.emailAddress,
            phoneNumber: user.phoneNumber,
        };
    }

    const methods = useForm({
    resolver: yupResolver(ProfileSchema),
    defaultValues,
  });

  const {
    handleSubmit,
    formState: { isSubmitting },
  } = methods;

  const onSubmit = async () => {
    navigate('/', { replace: true });
  };

  return (
    <FormProvider methods={methods} onSubmit={handleSubmit(onSubmit)}>
      <Stack spacing={3}>
        <Stack direction={{ xs: 'column', sm: 'row' }} spacing={2}>
          <RHFTextField name="firstName" label="First name"/>
          <RHFTextField name="lastName" label="Last name" />
        </Stack>
        <RHFTextField name="username" label="Username" />
        <RHFTextField name="email" label="Email address" />
        <RHFTextField name="phoneNumber" label="Phone Number"/>

        <LoadingButton fullWidth size="large" type="submit" variant="contained" loading={isSubmitting}>
          Update Profile
        </LoadingButton>
      </Stack>
    </FormProvider>
  );
}
