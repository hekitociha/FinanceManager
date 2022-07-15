import React, { Component } from 'react';
import { Navbar, Nav, FormControl, Container, Form, Button  } from 'react-bootstrap'
import logo from './money.jpg'
import './Style.css'

export default class Header extends Component {
    render() {
        return (
            <Navbar className="navbar">
                <Container className="container">
                    <Navbar.Brand href="/">
                        <img
                            src={logo }
                            height="30"
                            width="30"
                            className = "logo"
                        />
                    </Navbar.Brand>
                    <Navbar.Collapse id="responsive-navbar-nav">
                        <Nav className ="menu">
                            <Nav.Link href="/"> Главная </Nav.Link>
                            <Nav.Link href="/login"> Войти </Nav.Link>
                            <Nav.Link href="/signup"> Зарегистрироваться </Nav.Link>
                            <Nav.Link href="/contacts"> Контакты </Nav.Link>
                        </Nav>
                        <Form inline >
                            <FormControl
                                type="text"
                                placeholder="Поиск"
                                className="search"
                            />
                            <Button className="button"> Поиск </Button>
                        </Form>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
            )
    }
}