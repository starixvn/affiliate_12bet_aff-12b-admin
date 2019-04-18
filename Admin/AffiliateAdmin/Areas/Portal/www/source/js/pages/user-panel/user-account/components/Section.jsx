import React, { Component } from 'react';

const Section = (props) => {
    const { title, children, ...rest } = props;

    return (
        <fieldset {...rest}>
            <legend>
                <h6>{title}</h6>
            </legend>

            {children}
        </fieldset>
    );
}

export default Section;