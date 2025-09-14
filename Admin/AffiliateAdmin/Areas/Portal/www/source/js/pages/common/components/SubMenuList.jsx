import React from 'react';
import { NavLink } from 'react-router-dom'

export const SubMenuList = (props) => {
    const menuTitle = props.menuTitle || '';
    return (
        <div className='sub-menu-container'>
            <div className='d-inline-block'>
                <img src={props.img} />
            </div>
            <div className='d-inline-block align-middle'>
                <h5 className='mb-0 text-danger'>{menuTitle}</h5>
            </div>
            <div className={`${menuTitle ? 'mt-2' : 'd-inline-block'}`}>
                <ul className='nav'>
                    {props.menus.map((menu) => {
                        return (
                            <li className='nav-item' key={menu.label}>
                                <NavLink className={'nav-link'} activeClassName='active' to={menu.href}>{menu.label}</NavLink>
                            </li>
                        )
                    })}
                </ul>
            </div>
        </div>
    )
}

export default React.memo(SubMenuList);