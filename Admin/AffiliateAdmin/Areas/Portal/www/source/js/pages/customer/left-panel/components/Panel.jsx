import React from 'react';

const Panel = (props) => {
    const { heading, children, style, ...attributes } = props;
    return (
        <div id='left_submenu' {...attributes}>
            <div id='left_tab'>
                <ul>
                    <li><a href='javascript:;'><span>{heading}</span></a></li>
                </ul>
            </div>
            <div id='clear'>
            </div>
            <div id='Bleft_Sub0' style={{ ...style }}>
                {children}
            </div>
        </div>
    )
}

export default React.memo(Panel);