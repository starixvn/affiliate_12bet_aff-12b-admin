import React from 'react';
import { Link } from 'react-router-dom'

export const MenuList = (props) => {
    return (
        <div className='sub-menu-container'>
            <div className='d-inline-block'>
                <img src={props.img} />
            </div>
            <div className='d-inline-block'>
                <ul className='nav'>
                    {props.menus.map((menu, index) => {
                        const active = index === props.activeIndex ? "active" : "";
                        return (
                            <li className='nav-item' key={menu.label}>
                                <Link className={'nav-link ' + active} to={menu.href} onClick={() => props.handleClick(index)}>{menu.label}</Link>
                            </li>
                        )
                    })}
                </ul>
            </div>
        </div>
    )
}

export default React.memo(MenuList);