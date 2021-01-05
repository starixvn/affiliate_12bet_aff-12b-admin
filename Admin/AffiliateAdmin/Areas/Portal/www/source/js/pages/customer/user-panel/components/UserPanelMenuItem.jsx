import React from 'react';
import MyLink from '../../../common/components/MyLink';

export const UserPanelMenuItem = (props) => {
    const { item, isActive, onClickMenu } = props;

    return (
        <li key={item.key} className="up-item">
            <MyLink className={`upitem-link ${isActive ? 'active' : ''}`}
                onClick={() => onClickMenu(item.key)}>
                <span className="menu-text">{item.name}</span>
            </MyLink>
        </li>
    )
}

export default React.memo(UserPanelMenuItem);