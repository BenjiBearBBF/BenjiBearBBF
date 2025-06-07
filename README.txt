QT-Ball_01 Scene (Raw Version)

1. Add a Unity Sphere named 'QT-Ball'
2. Add two child Quads:
   - One on top (0, +0.51, 0), rotated (90, 0, 0) with Tick texture
   - One on bottom (0, -0.51, 0), rotated (-90, 0, 0) with Cross or No texture

3. Attach 'OrbitAroundTarget.cs' to Main Camera
   - Set the target to the QT-Ball object

4. Create a Canvas with:
   - Button labeled 'Change Nav' hooked to NavigationGatePanel.CycleNavMode()
   - Button labeled 'Change Projection' hooked to NavigationGatePanel.CycleProjection()
   - Two Text elements to display current nav and projection

This setup mimics orbiting around a ball to represent different quantum states/gates.