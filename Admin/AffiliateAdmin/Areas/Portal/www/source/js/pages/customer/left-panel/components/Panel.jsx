import React from 'react';

const Panel = (props) => {
    const { heading, children, style, ...attributes } = props;

    return (
        <div className='left-submenu' {...attributes}>
            <ul>
                <li><a href='javascript:;'><span>{heading}</span></a></li>
            </ul>
            <div className='content' style={{ ...style }}>
                {children}
            </div>
        </div>
    )
}

export default React.memo(Panel);