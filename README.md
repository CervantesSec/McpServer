# MCP Server for Cervantes

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4)](https://dotnet.microsoft.com/)
[![MCP](https://img.shields.io/badge/MCP-Model%20Context%20Protocol-blue)](https://modelcontextprotocol.io/)
[![License](https://img.shields.io/badge/License-AGPL--3.0-blue.svg)](LICENSE)
[![Build Status](https://img.shields.io/badge/build-passing-brightgreen)]()

A Model Context Protocol (MCP) server implementation that provides seamless integration with the Cervantes penetration testing management platform API. This server enables AI assistants like Claude to interact with Cervantes data through standardized MCP tools.

## 🎯 Purpose

This MCP server acts as a bridge between AI assistants and the [Cervantes](https://github.com/CervantesSec/cervantes) platform, allowing users to:

- Manage penetration testing projects, clients, and tasks through natural language
- Query and manipulate vulnerability data
- Generate reports and manage documentation
- Handle user and role management
- Interact with vault and notes systems

## 🛠 Technologies

- **.NET 9.0** - Core framework
- **Model Context Protocol** - Communication standard
- **HTTP Client** - REST API integration
- **Dependency Injection** - Service management

## ✨ Features

### 📋 Project Management
- Create, read, update, and delete projects
- Manage project members and permissions
- Handle project notes and attachments
- Generate executive summaries

### 🔍 Vulnerability Management
- Complete CRUD operations for vulnerabilities
- Vulnerability categorization and CWE mapping
- Risk assessment and status tracking
- Notes, targets, and attachments management
- Template system support

### 👥 Client Management
- Client information management
- Avatar upload support
- Project association

### 📝 Task Management
- Task creation and assignment
- Status tracking and updates
- Notes and attachments support
- Target association

### 📊 Reporting
- Report generation and management
- Template system
- Component management
- Multiple export formats

### 🔐 Security & Access
- User and role management
- Vault for secure credential storage
- Permission-based access control

### 📄 Documentation
- Document management system
- File upload and organization
- Version control support

## 🔧 Runtime Requirements

- **.NET 9.0 Runtime** or later
- **Cervantes** instance (running and accessible)
- **Network connectivity** to Cervantes API endpoints

## 📦 Installation


### From Source

1. **Clone the repository**
   ```bash
   git clone https://github.com/CervantesSec/McpServer.git
   cd mcpserver-cervantes
   ```

2. **Build the project**
   ```bash
   dotnet build --configuration Release
   ```

3. **Run the server**
   ```bash
   dotnet run --project McpServer
   ```

## ⚙️ Configuration

### appsettings.json

Configure the server by modifying the `appsettings.json` file:

```json
{
  "Cervantes": {
    "BaseUrl": "https://your-cervantes-instance.com",
    "Username": "your-username",
    "Password": "your-password",
    "AuthMethod": "BasicAuth"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### Environment Variables (Alternative)

You can also configure using environment variables:

```bash
# Cervantes API Configuration
CERVANTES__BASEURL=https://your-cervantes-instance.com
CERVANTES__USERNAME=your-username
CERVANTES__PASSWORD=your-password
CERVANTES__AUTHMETHOD=BasicAuth

# Logging Configuration
LOGGING__LOGLEVEL__DEFAULT=Information
```

### MCP Integration

Add the server to your MCP client configuration:

```json
{
  "servers": {
    "cervantes": {
      "command": "dotnet",
      "args": ["run", "--project", "/path/to/McpServer"],
      "env": {
        "CERVANTES_API_BASE_URL": "https://your-cervantes-instance.com"
      }
    }
  }
}
```

## 🚀 Usage

Once configured, the MCP server provides the following tool categories:

### Available Tools

- **ClientTool**: Manage client information and avatars
- **ProjectTool**: Handle projects, members, notes, and attachments
- **VulnerabilityTool**: Complete vulnerability lifecycle management
- **TaskTool**: Task management with notes, targets, and attachments
- **ReportTool**: Report generation and template management
- **DocumentTool**: Document management and organization
- **VaultTool**: Secure credential and sensitive data storage
- **NoteTool**: General note-taking and organization
- **UserTool**: User and role management
- **RoleTool**: Permission and access control management

### Example Usage with Claude

```
"Can you show me all high-risk vulnerabilities in the current project?"

"Create a new client named 'Acme Corp' with contact email admin@acme.com"

"Generate a penetration testing report for project ID abc-123"
```

## 🤝 How to Contribute

We welcome contributions! Here's how you can help:

1. **Fork the repository**
2. **Create a feature branch**
   ```bash
   git checkout -b feature/amazing-feature
   ```
3. **Make your changes and add tests**
4. **Commit your changes**
   ```bash
   git commit -m 'Add amazing feature'
   ```
5. **Push to your branch**
   ```bash
   git push origin feature/amazing-feature
   ```
6. **Open a Pull Request**

### Development Guidelines

- Follow C# coding conventions
- Add unit tests for new features
- Update documentation as needed
- Ensure all builds pass
- Use meaningful commit messages

## 🔒 Security

If you discover a security vulnerability, please send an email to [ruben.mesquida@owasp.org](mailto:ruben.mesquida@owasp.org). All security vulnerabilities will be promptly addressed.

### Security Features

- Secure API communication
- Input validation and sanitization
- Error handling without information disclosure
- Structured logging for audit trails

## 🐛 How to Report Bugs

If you find a bug, please create an issue on GitHub with:

- Clear description of the problem
- Steps to reproduce
- Expected vs actual behavior
- Environment details (.NET version, OS, etc.)
- Relevant logs or error messages

## 📚 Documentation

- [MCP Protocol Documentation](https://modelcontextprotocol.io/)
- [Cervantes API Documentation](https://github.com/CervantesSec/cervantes)
- [.NET 9.0 Documentation](https://docs.microsoft.com/dotnet/)

## 🏗️ Architecture

The server follows a modular architecture:

```
McpServer/
├── Models/          # Data models and ViewModels
├── Services/        # API client and business logic
├── Tools/           # MCP tool implementations
└── Program.cs       # Application entry point
```

### Key Components

- **CervantesApiClient**: HTTP client for API communication
- **Tool Classes**: Individual MCP tool implementations
- **Model Classes**: Data transfer objects and ViewModels
- **Dependency Injection**: Service registration and lifetime management

## 🔄 API Coverage

The server provides comprehensive coverage of the Cervantes API:

- ✅ Client Management
- ✅ Project Management
- ✅ Vulnerability Management
- ✅ Task Management
- ✅ Report Generation
- ✅ Document Management
- ✅ User & Role Management
- ✅ Vault Management
- ✅ Notes System

## 📊 Supported Operations

| Resource | Create | Read | Update | Delete | Special Operations                |
|----------|--------|------|--------|--------|-----------------------------------|
| Clients | ✅ | ✅ | ✅ | ✅ |                                   |
| Projects | ✅ | ✅ | ✅ | ✅ | Members, notes, attachments       |
| Vulnerabilities | ✅ | ✅ | ✅ | ✅ | Templates, categories, CWE        |
| Tasks | ✅ | ✅ | ✅ | ✅ | Notes, targets, attachments       |
| Reports | ✅ | ✅ | ✅ | ✅ | Templates, components, generation |
| Documents | ✅ | ✅ | ✅ | ✅ | Doc management                    |
| Vault | ✅ | ✅ | ✅ | ✅ |                                   |
| Users | ✅ | ✅ | ✅ | ✅ | Role assignment                   |

## 📈 Performance

- Async/await throughout for optimal performance
- Connection pooling for HTTP requests
- Structured logging for observability
- Graceful error handling and recovery

## 📝 License

This project is licensed under the GNU Affero General Public License v3.0 (AGPL-3.0) - see the [LICENSE](LICENSE) file for details.

### AGPL-3.0 License Summary

The AGPL-3.0 license ensures that:
- ✅ You can use, modify, and distribute this software freely
- ✅ You must provide source code for any modifications
- ✅ **Network use is considered distribution** - if you run this server for others to access, you must provide the source code
- ✅ Any derivative works must also be licensed under AGPL-3.0
- ✅ You must include copyright and license notices

This license is specifically chosen to ensure that improvements to this MCP server remain open source, even when deployed as a service.

## 🙏 Acknowledgments

- [CervantesSec](https://github.com/CervantesSec) for the excellent penetration testing platform
- [Anthropic](https://www.anthropic.com/) for the Model Context Protocol specification
- The .NET community for excellent tooling and libraries

## 📞 Support

- 📧 Email: [ruben.mesquida@owasp.org](mailto:ruben.mesquida@owasp.org)
- 💬 GitHub Issues: [Create an issue](https://github.com/CervantesSec/McpServer/issues)
- 📖 Documentation: [Wiki](https://cervantessec.org)

---

**Made with ❤️ for the cybersecurity community**