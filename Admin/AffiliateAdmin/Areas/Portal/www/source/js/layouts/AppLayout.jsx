import React, { PureComponent } from 'react';

export default class AppLayout extends PureComponent {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <div className="app-container">
        {this.props.children}
      </div>
    );
  }
}