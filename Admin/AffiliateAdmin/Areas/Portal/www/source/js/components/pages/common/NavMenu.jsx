import React from 'react';
import { connect } from 'react-redux';
import { Nav, NavItem, NavLink } from 'reactstrap';

const NavMenu = (props) => {
    const navMenuList = props.navMenuList || null;

    return navMenuList && (
        <div className="sub-menu-container">
            <Nav>
                {
                    navMenuList.map(item => {
                        return (
                            <NavItem>
                                <NavLink active={item.isActive} href="javascript:;" onClick={item.handleClick}>{item.title}</NavLink>
                            </NavItem>
                        )
                    })
                }
            </Nav>
        </div>
    );
}

export default connect()(React.memo(NavMenu));