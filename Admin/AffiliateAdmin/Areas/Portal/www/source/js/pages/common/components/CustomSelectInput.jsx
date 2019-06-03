import React, { PureComponent } from 'react';

export default class CustomSelectInput extends PureComponent {
    constructor(props) {
        super(props);

        this.selectInput = React.createRef();
    }

    focusSelectInput = () => {
        this.selectInput.current.focus();
    }

    render() {
        const { style, value, onChange, onBlur, children } = this.props;
        return (
            <select style={{ ...style }} value={value} onChange={onChange} onBlur={onBlur} ref={this.selectInput}>
                {children}
            </select>
        )
    }
}