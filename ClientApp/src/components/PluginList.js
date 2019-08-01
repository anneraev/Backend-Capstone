import React, { Component } from 'react';

export class PluginList extends Component {
  static displayName = PluginList.name;

  constructor (props) {
    super(props);
    this.state = { plugins: [], loading: true };

    fetch('api/Plugins')
      .then(response => response.json())
      .then(data => {
        this.setState({ plugins: data, loading: false });
      });
  }

  //render a list of plugins in a table with their corresponding title, author, category and engine.

  static renderPluginTable (plugins) {
    console.log(plugins);
    return (
      <table className='table table-striped'>
        <thead>
          <tr>
            <th>Title</th>
            <th>Category</th>
            <th>Author</th>
            <th>Engine</th>
          </tr>
        </thead>
        <tbody>
          {plugins.map(p =>
            <tr key={p.pluginId}>
              <td>{p.title}</td>
              <td>{p.pluginType.name}</td>
              <td>{p.user.name}</td>
              <td>{p.engine.title}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render () {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : PluginList.renderPluginTable(this.state.plugins);

    return (
      <div>
        <h1>Plugins</h1>
        <p>This is a list of all plugins.</p>
        {contents}
      </div>
    );
  }
}
