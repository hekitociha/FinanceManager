import React, { Component } from 'react';
import { Navbar, Nav, FormControl, Container, Form, Button  } from 'react-bootstrap'
import './Style.css'

export default class Header extends Component {
    render() {
        return (
            <div className="navbar">
                <div className="navbar left">
                    <a href="/"><img className="nav-img" src="../money.png" width="70" alt="ico"></img></a>
                    <a className="nav-link" href="/">Главная</a>
                    <a className="nav-link" href="/contacts" >Контакты</a>
                </div>
                <div className="navbar center">
                    <Form className="search">
                        <FormControl
                            type="text"
                            placeholder="Поиск"
                            className="search"
                        />
                    </Form>
                </div>
                <div className="navbar right">
                    <button className="button signin" onClick>Войти</button>
                    <button className="button" onClick>Зарегистрироваться</button>
                </div>
            </div>
            )
    }
}