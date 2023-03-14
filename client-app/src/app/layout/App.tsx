import React, { Fragment } from 'react';
import { Container } from 'semantic-ui-react';
import NavBar from './NavBar';
import { observer } from 'mobx-react-lite';
import { Outlet, useLocation } from 'react-router-dom';
import HomePage from '../../features/home/hopePage';

function App() {
  const loaction = useLocation();

  return (
    <>
      {loaction.pathname === '/' ? <HomePage /> : (
        <>
          <NavBar/>
          <Container style={{marginTop: '7em'}}>
            <Outlet />
          </Container>
        </>
      )}
    </>
  );
}

export default observer(App);
