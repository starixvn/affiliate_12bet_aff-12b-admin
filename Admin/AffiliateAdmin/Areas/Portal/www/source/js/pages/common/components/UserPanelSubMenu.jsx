import React from 'react';
import { Nav, NavItem, NavLink } from 'reactstrap';

const UserPanelSubMenu = (props) => {
    const subMenuList = props.subMenuList || null;

    const handleClick = (event, func) => {
        event.preventDefault();

        func();
    }

    return subMenuList && (
        <div className="sub-menu-container pt-1">
            <Nav>
                {
                    subMenuList.map(item => {
                        return (
                            <NavItem key={item.title}>
                                <NavLink active={item.isActive} href="#" onClick={(e) => handleClick(e, item.handleClick)}>{item.title}</NavLink>
                            </NavItem>
                        )
                    })
                }
            </Nav>
        </div>
    );
}

export default React.memo(UserPanelSubMenu);